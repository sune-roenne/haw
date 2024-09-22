using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThousandAcreWoods.Book.Configuration;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.Book.Generator;
public static class LatexBookGenerator
{
    private const string DefaultColorNos = "0,0,0";
    private static Regex RgbRegex = new Regex(@"([0-9]+.[0-9]+.[0-9]+)");
    private static Regex PercentTagRegex = new Regex("%[A-Z]+", RegexOptions.IgnoreCase);
    private const string ChaptersFolder = "chapters";
    private static readonly Regex NumberRegex = new Regex(@"[0-9]+");

    public static async Task<string> GenerateLatexBook(this BookRelease book, BookConfiguration conf, bool withTheLir)
    {
        var targetDir = Path.Combine(conf.GenerationOutputDirectory, book.Version);
        if(Directory.Exists(targetDir))
            Directory.Delete(targetDir, true);
        Directory.CreateDirectory(targetDir);
        Directory.CreateDirectory(Path.Combine(targetDir, ChaptersFolder));
        await book.GenerateFrontPage(conf, targetDir);
        await CopyOtherFiles(conf, targetDir);

        var mainTexFileContent = await File.ReadAllTextAsync(Path.Combine(conf.TemplateDirectory, GenerationConstants.Files.MainTexFileName));

        var colorsString = book.GenerateCharacterColors(withTheLir);
        mainTexFileContent = mainTexFileContent.Replace(GenerationConstants.Tags.ColorInsertTag, colorsString);
        var interactionCommands = book.GenerateCharacterInteractionCommands(withTheLir);
        mainTexFileContent = mainTexFileContent.Replace(GenerationConstants.Tags.CharacterCommandInsertTag, interactionCommands);


        var (chapterTemplateStart, chapterTemplateEnd) = (
            mainTexFileContent.IndexOf(GenerationConstants.Tags.ChapterTemplateStartTag),
            mainTexFileContent.IndexOf(GenerationConstants.Tags.ChapterTemplateEndTag)
            );

        var chapterTemplate = mainTexFileContent.Substring(
            chapterTemplateStart,
            chapterTemplateEnd - chapterTemplateStart
            );
        chapterTemplate = PercentTagRegex.Replace(chapterTemplate, "");
        var chapterContents = new StringBuilder();
        foreach(var (chapter, chapterIndex) in book.Chapters.Select((_,indx) => (_,indx)))
        {
            await chapter.GenerateChapter(book, conf, targetDir, chapterContents, chapterTemplate, chapterIndex);

        }
        var mainTexFileContentBuilder = new StringBuilder(mainTexFileContent.Substring(0, chapterTemplateStart));
        mainTexFileContentBuilder.AppendLine(chapterContents.ToString());
        mainTexFileContentBuilder.AppendLine(TheEndString(book.GenerateForAboutTheAuthor()));
        mainTexFileContentBuilder.AppendLine(@"\end{document}");

        var mainTexFileOutputFileName = Path.Combine(targetDir, GenerationConstants.Files.MainTexFileName);
        await File.WriteAllTextAsync(mainTexFileOutputFileName, mainTexFileContentBuilder.ToString());
        return mainTexFileOutputFileName;

    }



    public static async Task GenerateFrontPage(this BookRelease book, BookConfiguration conf, string outputFolder)
    {
        var templateContent = await File.ReadAllTextAsync(Path.Combine(conf.TemplateDirectory, GenerationConstants.Files.FrontPageTexFileName));
        var replacedContent = templateContent
            .Replace(GenerationConstants.Tags.AuthorTag, book.Author)
            .Replace(GenerationConstants.Tags.VersionTag, book.Version);
        await File.WriteAllTextAsync(Path.Combine(outputFolder, GenerationConstants.Files.FrontPageTexFileName), replacedContent);
    }

    public static async Task GenerateChapter(this BookChapter chapter, BookRelease book, BookConfiguration conf, string outputFolder, StringBuilder mainFileContent, string chapterTemplate, int chapterIndex)
    {
        var chapterFileContent = chapter.ChapterContents.GenerateForContents(chapterIndex);
        var outputFileName = $"{chapter.ChapterDate.ToString("yyyy-MM-dd")}{chapter.ChapterOrderToUse}-{chapter.ChapterName}.tex";
        var absouluteOutputFileName = Path.Combine(outputFolder, ChaptersFolder, outputFileName);

        await File.WriteAllTextAsync(absouluteOutputFileName, chapterFileContent.ToString());

        var chapterImage = chapter.MetaData?.StoryLineKey?.PipeOpt(stk => book.StoryLines.GetValueOrDefault(stk.ToLower().Trim())?.Image) ?? conf.DefaultChapterImage;
        var chapterInsert = chapterTemplate
            .Replace(GenerationConstants.Tags.ChapterTitleTag, chapter.ChapterTitleToUse)
            .Replace(GenerationConstants.Tags.ChapterImageTag, chapterImage)
            .Replace(GenerationConstants.Tags.ChapterDateTag, chapter.ChapterDateToUse.AsFormattedChapterDate());

        mainFileContent.AppendLine(chapterInsert);
        mainFileContent.AppendLine(@$"\input{{{ChaptersFolder}/{outputFileName}}}");
    }

    private static string GenerateForAboutTheAuthor(this BookRelease book)
    {
        var generated = book.AboutTheAuthor.Contents.GenerateForContents(2_000);
        return generated;
    }

    private static string GenerateForContents(this IEnumerable<BookChapterContent> conts, int chapterIndex)
    {

        var chapterFileContent = new StringBuilder();
        var isDialogue = false;
        var setDialogue = (bool shouldBeDialogue) =>
        {
            if (isDialogue == shouldBeDialogue)
                return;
            if (isDialogue && !shouldBeDialogue)
                chapterFileContent.AppendLine(@"\end{dialogue}");
            else chapterFileContent.AppendLine(@"\begin{dialogue}");
            isDialogue = shouldBeDialogue;
        };
        foreach (var cont in conts)
        {
            if (cont is BookContextBreak brk)
            {
                setDialogue(false);
                chapterFileContent.AppendLine(@"\newpage");

            }
            else if (cont is BookChapterSection secc)
            {
                setDialogue(false);
                chapterFileContent.AppendLine($@"\subsection*{{{secc.Title}}}");
            }
            else if (cont is BookNarration narr)
            {
                setDialogue(false);
                chapterFileContent.AppendLine(narr.NarrationContent.TexSanitize());
                chapterFileContent.AppendLine(@"\paragraph{}");
            }
            else if (cont is BookCharacterLine lin && !lin.IsThought)
            {
                setDialogue(true);
                foreach (var (linCont, indx) in lin.LineParts.Select((_, indx) => (_, indx)))
                {
                    var text = linCont.PartText.TexSanitize();
                    if (text.Length == 0)
                        text = "...";
                    var appendee = (indx, linCont.Description) switch
                    {
                        (0, null) => @$"\{lin.Character.ToChapterCharacter(chapterIndex).TexSayStartName()}{{{lin.Character.CharacterName.TexSanitize()}}}{{{text}}}",
                        (0, string desc) => @$"\{lin.Character.ToChapterCharacter(chapterIndex).TexSayStartNameDesc()}{{{lin.Character.CharacterName.TexSanitize()}}}{{{text}}}{{{desc.TexSanitize()}}}",
                        (_, null) => @$"\{lin.Character.ToChapterCharacter(chapterIndex).TexSayContinueName()}{{{text}}}",
                        (_, string desc) => @$"\{lin.Character.ToChapterCharacter(chapterIndex).TexSayContinueNameDesc()}{{{text}}}{{{desc.TexSanitize()}}}"
                    };
                    chapterFileContent.AppendLine(appendee);
                }
            }
            else if (cont is BookCharacterLine th && th.IsThought)
            {
                setDialogue(false);
                chapterFileContent.AppendLine(@$"{{\color{{{th.Character.ToChapterCharacter(chapterIndex).TexColorName()}}} \begin{{adjustwidth}}{{2em}}{{0pt}}");
                chapterFileContent.AppendLine(@$"\textit{{{th.Character.CharacterName.ToUpper()} (thinking)}}:\\");
                foreach (var thLin in th.LineParts)
                    chapterFileContent.AppendLine(@$"\textit{{{thLin.PartText.TexSanitize()}}}{(thLin.PartText.Length > 0 ? @"\\" : "")}");

                chapterFileContent.AppendLine(@"\end{adjustwidth}}");
                chapterFileContent.AppendLine(@"\paragraph{}");

            }
            else if (cont is BookSinging sing)
            {
                setDialogue(false);
                isDialogue = false;
                var color = sing.Character.ToChapterCharacter(chapterIndex).TexColorName();
                var lyrics = sing.LinesSong
                    .ToList()
                    .BreakIntoStanzas()
                    .Where(_ => _.Line.Length > 0)
                    .Select(_ => _.Line + @"\\" + (_.IsLastLine ? "!" : ""))
                    .MakeString(@"\begin{verse}" + "\r\n", "\r\n", "\r\n" + @"\end{verse}");
                chapterFileContent.AppendLine("\r\n" + @$"{{\color{{{color}}} {sing.Character.CharacterName.TexSanitize().ToUpper()} [singing]:\\{"\r\n"}{lyrics}}}");
            }
            else if (cont is BookNarrationList lis)
            {
                setDialogue(false);
                if (lis.IsNumbered)
                    chapterFileContent.AppendLine("\r\n" + @"\begin{enumerate}");
                else
                    chapterFileContent.AppendLine("\r\n" + @"\begin{itemize}");
                foreach (var it in lis.Items)
                {
                    var text = it.TexSanitize()
                        .Replace("[", @"\textbf{")
                        .Replace("]", @": } ");
                    chapterFileContent.AppendLine(@$"\item {text}");

                }
                if (lis.IsNumbered)
                    chapterFileContent.AppendLine("\r\n" + @"\end{enumerate}");
                else
                    chapterFileContent.AppendLine("\r\n" + @"\end{itemize}");
            }
            else if (cont is BookCharacterStoryTime st)
            {
                setDialogue(false);
                var story = st.Paragrahs
                    .Select(_ => _ switch
                    {
                        BookCharacterStoryTime.BookCharacterStoryTimeItemListParagraph itLis =>
                           $"\\begin{{itemize}}\r\n{itLis.Items.Select(_ => $"\\item {_.TexSanitize()}").MakeString("\r\n")}\r\n\\end{{itemize}}",
                        BookCharacterStoryTime.BookCharacterStoryTimeTextParagraph tx => tx.Text.TexSanitize(),
                        _ => throw new Exception("YO! WTF!!??")
                    })
                    .MakeString("\r\n\\paragraph{}");

                chapterFileContent.AppendLine(
                    "\r\n" +
                    $@"\storytime{{{st.Title.TexSanitize()}}}{{{st.Character.ToChapterCharacter(chapterIndex).TexColorName()}}}{{{story}}}" +
                    "\r\n"
                );
            }

        }
        setDialogue(false);
        return chapterFileContent.ToString();

    }




    private static async Task CopyOtherFiles(BookConfiguration conf, string outputDir)
    {
        var sourceFolder = conf.TemplateDirectory;
        var relevantFiles = Directory.GetFiles(sourceFolder)
            .Where(_ => !_.ToLower().Contains(GenerationConstants.Files.FrontPageTexFileName.ToLower()))
            .Where(_ => !_.ToLower().Contains(GenerationConstants.Files.MainTexFileName.ToLower()))
            .ToList();
        foreach (var fil in relevantFiles)
        {
            var content = await File.ReadAllBytesAsync(fil);
            var outputFileName = Path.Combine(outputDir, Path.GetFileName(fil));
            await File.WriteAllBytesAsync(outputFileName, content);
        }
    }


    private static string GenerateCharacterInteractionCommands(this BookRelease book, bool withTheLir)
    {
        var allCharacters = book.AllCharacters();
        var charactorColorMap = book.CharacterColorMap();
        var characterFontMap = book.CharacterFontMap();
        var characterData = allCharacters
            .DistinctBy(_ => (_.ChapterCharacterId))
            .Select(_ => (_.ChapterCharacterId, CharacterName: _.Character.CharacterName, ColorName: charactorColorMap[_.ChapterCharacterId].ColorName, FontFamily:  (characterFontMap.TryGetValue(_.ChapterCharacterId, out var font) && withTheLir) ? font : null))
            .OrderBy(_ => _.ChapterCharacterId)
            .ToList();
        var trueAndFalse = new List<bool> { true, false };
        var parameterOptions = (from isStart in trueAndFalse
                                from addDesc in trueAndFalse
                                select (isStart, addDesc))
                              .ToList();
        var allCommands = parameterOptions
            .Select(opt => GenerateInteractionCommands(characterData, opt.isStart, opt.addDesc))
            .MakeString("\r\n");
        return allCommands;
    }

    private static string GenerateInteractionCommands(
        IEnumerable<(string CharacterFormatKey, string CharacterName, string ColorName, string? FontFamily)> characters,
        bool isStart,
        bool addDescription
        ) => GenerateInteractionCommands(
            characters,
            dat => (isStart, addDescription) switch
            {
                (true, true) => @$"\newcommand{{\{dat.CharacterFormatKey.TexSayStartNameDesc()}}}[3]{{{
                    $"{"#1".ToCommand("speak")} {
                        dat.FontFamily
                           .PipeOpt(SelectFont)
                           .OptWrap(dat.ColorName
                              .ToCommand("color")
                              .Wrap("#2"))
                        }"
                    } \direct{{#3}} }}",
                (true, false) => @$"\newcommand{{\{dat.CharacterFormatKey.TexSayStartName()}}}[2]{{{
                    $"{"#1".ToCommand("speak")} {dat.FontFamily
                           .PipeOpt(SelectFont)
                           .OptWrap(dat.ColorName
                              .ToCommand("color")
                              .Wrap("#2"))
                        }"
                    }}}",
                (false, true) => @$"\newcommand{{\{dat.CharacterFormatKey.TexSayContinueNameDesc()}}}[2]{{\\ {
                    $"{dat.FontFamily
                           .PipeOpt(SelectFont)
                           .OptWrap(dat.ColorName
                              .ToCommand("color")
                              .Wrap("#1"))}"} \direct{{#2}} }}",
                (false, false) => @$"\newcommand{{\{dat.CharacterFormatKey.TexSayContinueName()}}}[1]{{ \\ {
                    $"{dat.FontFamily
                           .PipeOpt(SelectFont)
                           .OptWrap(dat.ColorName
                              .ToCommand("color")
                              .Wrap("#1"))}"}}}"
            }
            );

    private static string ToCommand(this string command, string wrapIn) => $@"\{wrapIn}{{{command}}}";

    private static string Wrap(this string command, string inner) => $@"{{{command} {inner}}}";
    private static string OptWrap(this string? command, string toWrap) => command switch
    {
        null => toWrap,
        _ => $@"{{{command} {toWrap}}}"
    };
        



    private static string SelectFont(string fontFamily) => $@"\setmainfont{{{fontFamily}}}";

    private static string GenerateInteractionCommands(
        IEnumerable<(string CharacterFormatKey, string CharacterName, string ColorName, string? FontFamily)> characters,
        Func<(string CharacterFormatKey, string CharacterName, string ColorName, string? FontFamily), string> commandConverter
        ) => characters
            .Select(commandConverter)
            .MakeString("\r\n");




    private static string GenerateCharacterColors(this BookRelease book, bool withTheLir)
    {

        var allCharacterKeys = book.CharacterColorMap();

        var colors = allCharacterKeys
            .Values
            .DistinctBy(_ => _.ColorName)
            .OrderBy(_ => _.ColorName)
            .Select(_ => @$"\definecolor{{{_.ColorName}}}{{RGB}}{{{(withTheLir ?  _.ColorNos : "0,0,0")}}}")
            .MakeString("\r\n");
        return colors;
    }

    private static string ColorNosFrom(string color)
    {
        var allMatches = RgbRegex.Matches(color);
        return allMatches.First().Value;
    }

    private static string TexSayStartName(this ChapterCharacter character) => TexSayStartName(character.ChapterCharacterId);
    private static string TexSayContinueName(this ChapterCharacter character) => TexSayContinueName(character.ChapterCharacterId);
    private static string TexSayStartNameDesc(this ChapterCharacter character) => TexSayStartNameDesc(character.ChapterCharacterId);
    private static string TexSayContinueNameDesc(this ChapterCharacter character) => TexSayContinueNameDesc(character.ChapterCharacterId);


    private static string TexSayStartName(this string chapterCharacterId) => $"charactersay{TexNameForCharacter(chapterCharacterId)}start";
    private static string TexSayContinueName(this string chapterCharacterId) => $"charactersay{TexNameForCharacter(chapterCharacterId)}continue";

    private static string TexSayStartNameDesc(this string chapterCharacterId) => $"charactersay{TexNameForCharacter(chapterCharacterId)}startdesc";
    private static string TexSayContinueNameDesc(this string chapterCharacterId) => $"charactersay{TexNameForCharacter(chapterCharacterId)}continuedesc";


    private static string TexColorName(this ChapterCharacter character) => TexColorName(character.ChapterCharacterId);
    private static string TexColorName(string characterFormatKey) => $"col{TexNameForCharacter(characterFormatKey)}";

    private static string TexName(this ChapterCharacter character) => TexNameForCharacter(character.ChapterCharacterId);

    private static string TexNameForCharacter(string characterKey) =>
        characterKey
        .Replace(" ", "")
        .Replace("&", "")
        .Replace(",", "")
        .Replace("1", "one")
        .Replace("2", "two")
        .Replace("3", "three")
        .Replace("4", "four")
        .Replace("5", "five")
        .Replace("6", "six")
        .Replace("7", "seven")
        .Replace("8", "eight")
        .Replace("9", "nine")
        .Replace("0", "zero");

    private static string TexSanitize(this string str) => str
        .Replace("&", @"\&")
        .Replace("%", @"\%")
        .Replace("#", @"\#")
        .Replace("¤", @"{\textnumero}");


    private static IReadOnlyCollection<ChapterCharacter> AllCharacters(this BookRelease book) => book.Chapters
       .SelectMany((chap,chapIndx) => chap.ChapterContents
            .Select(con => con switch
                {
                    BookCharacterLine lin => new ChapterCharacter(lin.Character, chapIndx),
                    BookSinging sin => new ChapterCharacter(sin.Character, chapIndx),
                    BookCharacterStoryTime st => new ChapterCharacter(st.Character, chapIndx),
                    _ => null
                } )
            .Collect()
            .DistinctBy(_ => _.ChapterCharacterId)
        ).ToList();  


    private static IReadOnlyDictionary<string, CharacterColorInfo> CharacterColorMap(this BookRelease book)
    {
        var allCharacters = book.AllCharacters();
        var charactorColorMap = allCharacters
            .Where(_ => _.Character.CharacterInfo?.Color != null)
            .GroupBy(_ => _.ChapterCharacterId)
            .Select(_ => (ChapterCharacterId: _.Key, Color: ColorNosFrom(_.First().Character.CharacterInfo!.Color)))
            .ToDictionary(_ => _.ChapterCharacterId, _ => _.Color);

        var returnee = allCharacters
            .DistinctBy(_ => _.ChapterCharacterId)
            .Select(_ => new CharacterColorInfo(_.ChapterCharacterId, ColorName: _.TexColorName(), ColorNos: charactorColorMap.GetValueOrDefault(_.ChapterCharacterId, DefaultColorNos)))
            .ToDictionarySafe(_ => _.CharacterFormatKey);
        return returnee;
    }

    private static IReadOnlyDictionary<string, string> CharacterFontMap(this BookRelease book)
    {
        var allCharacters = book.AllCharacters();
        var returnee = allCharacters
            .Where(_ => _.Character.CharacterInfo?.Font != null)
            .GroupBy(_ => _.ChapterCharacterId)
            .Select(_ => (CharacterFormatKey: _.Key, Font: _.First().Character.CharacterInfo!.Font))
            .ToDictionary(_ => _.CharacterFormatKey, _ => _.Font!);
        return returnee;

    }



    private record CharacterColorInfo(string CharacterFormatKey, string ColorName, string ColorNos);

    private record ChapterCharacter(BookCharacter Character, int ChapterNo)
    {
        public string ChapterCharacterId = $"{Character.CharacterKey}{ChapterNo}";
    }

    private static ChapterCharacter ToChapterCharacter(this BookCharacter character, int chapterIndex) => new ChapterCharacter(character, chapterIndex);

    private static List<(string Line, bool IsLastLine)> BreakIntoStanzas(this List<string> lines) => lines switch
    {
        [] => [],
        [string car, "", .. List<string> cldr] => [(car, true), .. BreakIntoStanzas(cldr)],
        [string car, .. List<string> cldr] => [(car, false), .. BreakIntoStanzas(cldr)]
    };

    private static readonly CultureInfo EnUs = new CultureInfo("en-US");
    private static string AsFormattedChapterDate(this DateTime date) => date.ToString("dddd", EnUs)
        .Pipe(weekDay => weekDay[0].ToString().ToUpper() + weekDay.Skip(1).MakeString("").ToLower())
        .Pipe(weekDay => (date.Day switch
            {
                1 => "1st",
                2 => "2nd",
                3 => "3rd",
                21 => "21st",
                22 => "22nd",
                23 => "23rd",
                31 => "31st",
                int d => $"{d}th"
            })
               .Pipe(dayOfMonth => $"{weekDay}, {date.ToString("MMMM", EnUs)} {dayOfMonth}, {date.ToString("yyyy")}")
        );


    private static string TheEndString(string aboutTheAuthorText) => $@"
\clearpage 
\vspace*{{\fill}} 
\begin{{center}} 
\begin{{minipage}}
{{\textwidth}} 
\centering{{
{{\setmainfont{{Trade Winds}}
  {{\huge To Be Continued}}\\
 {{\large (probably)}}
}}
}} 
\end{{minipage}} \end{{center}} 
\vfill % equivalent to \vspace{{\fill}} 
\clearpage

{{\setmainfont{{Trade Winds}}
\section*{{About}}

{aboutTheAuthorText}

}}


";


}
