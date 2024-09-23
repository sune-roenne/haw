using ThousandAcreWoods.Language.Extensions;
using System.Text;
using System.Text.RegularExpressions;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Helpers;
internal static class BookInputAnalysis
{

    private static readonly IReadOnlyCollection<(Regex Regex, string Style)> StyleMatchers = [
        (new Regex("(tired smile)"), SsmlConstants.VoiceStyles.Disgruntled),
        (new Regex("(sadistic)"), SsmlConstants.VoiceStyles.Excited),
        (new Regex("(professional)"), SsmlConstants.VoiceStyles.CustomerService),

        (new Regex("(folding)|(conced)|(forfeit)"), SsmlConstants.VoiceStyles.Envious),

        (new Regex("(comfort)"), SsmlConstants.VoiceStyles.Empathetic),

        (new Regex("(smiling)|(smile)"), SsmlConstants.VoiceStyles.Friendly),

        (new Regex("(pensive)|(think)|(shaking .* head)|(thought)|(trailing off)"), SsmlConstants.VoiceStyles.Serious),
        (new Regex("(serious)"), SsmlConstants.VoiceStyles.Serious),

        (new Regex("(deadpan)|(stern)"), SsmlConstants.VoiceStyles.Unfriendly),

        (new Regex("(whisper)"), SsmlConstants.VoiceStyles.Whispering),


        (new Regex("(shocked)"), SsmlConstants.VoiceStyles.Fearful),

        (new Regex("(shout)"), SsmlConstants.VoiceStyles.Shouting)

        ];

    private static string? StyleFor(this string description) => StyleMatchers
        .FirstOrDefault(_ => _.Regex.Matches(description).Any()).Style;


    public static void Analyze(BookRelease release)
    {
        AnalyzeDescriptions(release);
    }

    private static void AnalyzeDescriptions(this BookRelease book)
    {
        var allDescriptions = book.Chapters
            .SelectMany(chap => chap.ChapterContents
                .SelectMany(cont => cont switch
                {
                    BookCharacterLine lin => lin.LineParts.Select(_ => _.Description).Where(_ => _ != null).Select(_ => _!),
                    _ => []
                }));
        var grouped = allDescriptions
            .GroupBy(_ => _)
            .Select(_ => (_.Key, Count: _.Count()))
            .OrderByDescending(_ => _.Count)
            .ToList();

        var withStyle = grouped
            .Select(_ => (Description: _.Key, _.Count, Style: _.Key.StyleFor()))
            .ToList();


        var mappedResults = withStyle
            .Where(_ => _.Style != null)
            .Select(_ => (_.Description, _.Count, Style: _.Style!))
            .GroupBy(_ => _.Style)
            .Select(_ => (Style: _.Key, Descriptions: _.OrderByDescending(_ => _.Count).ToList(), TotalCount: _.Sum(_ => _.Count)))
            .OrderByDescending(_ => _.TotalCount)
            .ToList();

        var result = new StringBuilder();

        foreach(var mappedRes in mappedResults)
        {
            result.AppendLine(mappedRes.Style + ": " + mappedRes.Style);
            foreach (var desc in mappedRes.Descriptions)
                result.AppendLine("  " + desc.Description + ": " + desc.Count);
        }


        foreach (var grp in withStyle.Where(_ => _.Style == null))
        {
            result.AppendLine($"{grp.Description}: {grp.Count}");
        }

        var outputFile = $"c:/temp2/desc-analysis-{DateTime.Now.ToFileTime()}.txt";
        File.WriteAllText(outputFile, result.ToString());


    }

    public static void SearchForSubstring(string folder, string toSearchFor)
    {
        foreach(var file in Directory.GetFiles(folder))
        {
            var fileLines = File.ReadLines(file);
            foreach(var (line, lineNo) in fileLines.Select((lin,indx) => (lin, indx+ 1)))
            {
                if (line.Contains(toSearchFor))
                    Console.WriteLine($"{file}: line {lineNo}: {line}");
            }

        }

    }

    private static readonly Regex DoubleCharRegex = new Regex(@"([A-Z])\1{2,}", RegexOptions.IgnoreCase);

    public static void SearchForDoubleCharacters(BookRelease book)
    {
        foreach(var chap in book.Chapters)
        {
            foreach(var cont in chap.ChapterContents.Collect(_ => _ as BookCharacterLine))
            {
                foreach(var part in cont.LineParts)
                {
                    foreach(var match in DoubleCharRegex.Matches(part.PartText).Select(_ => _))
                    {
                        Console.WriteLine($"{chap.ChapterName} {chap.ChapterDate.ToString("dd-MM-yyyy")}: {part}");
                    }
                }
            }
        }
    }





}
