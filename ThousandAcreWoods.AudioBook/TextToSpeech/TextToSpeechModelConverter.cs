using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;
using ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech;
internal static class TextToSpeechModelConverter
{

    internal static SsmlEntry ToSsmlEntry(
        this BookChapterContent cont,
        BookChapter chap,
        TextToSpeechVoiceDefinition narrator,
        TextToSpeechVoiceConfiguration? narratorConfig,
        TextToSpeechVoiceDefinition? characterVoice,
        TextToSpeechVoiceConfiguration? characterConfiguration,
        string? personDistinguisher) => cont.SemanticId(chap, personDistinguisher)
          .Pipe(semanticId => new VoiceAndConfig(narrator, narratorConfig)
             .Pipe(narrVoi => (cont, characterVoice.PipeOpt(vo => new VoiceAndConfig(vo, characterConfiguration))) switch
          {
              (BookCharacterLine lin, VoiceAndConfig vo)  => lin.ToSsmlModel(vo, semanticId),
              (BookCharacterStoryTime st, VoiceAndConfig vo) => st.ToSsmlModel(vo, semanticId),
              (BookContextBreak br,_) =>  br.ToSsmlModel(semanticId, narrator),
              (BookNarration narr,_) => narr.ToSsmlModel(narrVoi, semanticId),
              (BookNarrationList lis,_) => lis.ToSsmlModel(narrVoi, semanticId),
              (BookSinging sin, VoiceAndConfig vo) => sin.ToSsmlModel(vo, semanticId),
              (BookChapterSection sec, _) => sec.ToSsmlModel(narrator, semanticId),
              _ => throw new NotImplementedException()

          }));


    internal static SsmlEntry ToChapterTitle(this BookChapter chap, int chapterIndex, TextToSpeechVoiceDefinition narrator, TextToSpeechVoiceConfiguration? narratorConfig) => new SsmlEntry(
            SemanticId: $"chapter_{chapterIndex.ToString("000", CultureInfo.InvariantCulture)}",
            PauseInMillis: null,
            CharacterKey: null,
            Voice: narrator,
            Configuration: (narratorConfig ?? new TextToSpeechVoiceConfiguration()) with
            {
                Style = SsmlConstants.VoiceStyles.NarrationProfessional
            },
            Content: [
                new SsmlLine($"Chapter {chapterIndex+1}",Configuration: null,SpeakerNameAnnounce: null),
                new SsmlLine($"<say-as interpret-as=\"date\" format=\"dmy\">{chap.ChapterDateToUse.ToString("dd-MM-yyyy")}</say-as>",Configuration: null, SpeakerNameAnnounce: null),
                new SsmlLine($"{chap.ChapterTitleToUse}",Configuration: null,SpeakerNameAnnounce: null),
                ]
            );


    private static SsmlEntry ToSsmlModel(this BookChapterSection sec, TextToSpeechVoiceDefinition narrator, string semanticId) => new SsmlEntry(
        SemanticId: semanticId,
        PauseInMillis: null,
        CharacterKey: null,
        Voice: narrator,
        Configuration: null,
        Content: sec.Title.ToSsmlContent(speakerName: null)
        );

    private static SsmlEntry ToSsmlModel(this BookCharacterLine lin, VoiceAndConfig voice, string semanticId) => new SsmlEntry(
        SemanticId: semanticId,
        PauseInMillis: lin.LineParts.PauseInMillis(),
        CharacterKey: lin.Character.CharacterVoiceKey(),
        Voice: voice.Voice,
        Configuration: voice.Config,
        Content: lin.LineParts
           .Select((_, indx) => new SsmlLine(
               SsmlText: _.PartText, 
               Configuration: _.StyleFor().PipeOpt(
                   styl => new TextToSpeechVoiceConfiguration(Style: styl)
               ) ?? voice.Config,
               SpeakerNameAnnounce: indx == 0 ? lin.Character.CharacterName : null,
               Description: _.Description
               )
           )
           .ToList()
        );

    private static int? PauseInMillis(this IEnumerable<BookCharacterLinePart> parts) => parts
        .Select(_ => $"{_.PartText.Replace(".","")}{_.Description ?? ""}")
        .MakeString("")
        .Pipe(_ => _.Length < 3 ? ((int?) SsmlConstants.Pauses.MediumPause.DurationInMilliseconds) : null);

    private static SsmlEntry ToSsmlModel(this BookCharacterStoryTime part, VoiceAndConfig voice, string semanticId) => new SsmlEntry(
        SemanticId: semanticId,
        PauseInMillis: null,
        CharacterKey: part.Character.CharacterVoiceKey(),
        Voice: voice.Voice,
        Configuration: new TextToSpeechVoiceConfiguration(Style: SsmlConstants.VoiceStyles.Documentary) ?? voice.Config,
        Content: part.Story.ToSsmlContent(speakerName: null)
        );

    private static SsmlEntry ToSsmlModel(this BookContextBreak lin, string semanticId, TextToSpeechVoiceDefinition narrator) => 
        new SsmlEntry(
            SemanticId: semanticId,
            PauseInMillis: SsmlConstants.Pauses.LongPause.DurationInMilliseconds,
            CharacterKey: null,
            Voice: narrator,
            Configuration: null,
            Content: [SsmlConstants.Pauses.LongPause]
            );

    private static SsmlEntry ToSsmlModel(this BookNarration narra, VoiceAndConfig narrator, string semanticId) => new SsmlEntry(
        SemanticId: semanticId,
        PauseInMillis: null,
        CharacterKey: null,
        Voice: narrator.Voice,
        Configuration: narrator.Config,
        Content: narra.NarrationContent.ToSsmlContent(speakerName: null ,narrator.Config)
        );


    private static SsmlEntry ToSsmlModel(this BookNarrationList lis, VoiceAndConfig narrator, string semanticId) => new SsmlEntry(
        SemanticId: semanticId,
        PauseInMillis: null,
        CharacterKey: null,
        Voice: narrator.Voice,
        Configuration: (narrator.Config ?? new TextToSpeechVoiceConfiguration()).Pipe(conf => conf with
        {
            Style = SsmlConstants.VoiceStyles.NewscastCasual
        }),
        Content: lis.Items
           .Select(it => new SsmlLine(SsmlText: it, Configuration: null, SpeakerNameAnnounce: null, Description: null))
           .ToList()
        );




    private static SsmlEntry ToSsmlModel(this BookSinging sin, VoiceAndConfig voice, string semanticId) => new SsmlEntry(
        SemanticId: semanticId,
        PauseInMillis: null,
        CharacterKey: sin.Character.CharacterVoiceKey(),
        Voice: voice.Voice,
        Configuration: (voice.Config ?? new TextToSpeechVoiceConfiguration()) with
        {
            Style = SsmlConstants.VoiceStyles.Poetry
        },
        Content: sin.LinesSong.ToSsmlContent(sin.Character.CharacterName)
        );



    private static string CharacterVoiceKey(this BookCharacter cha) => cha.CharacterFormatKey;

    private static IReadOnlyCollection<SsmlContent> ToSsmlContent(this string str, string? speakerName, TextToSpeechVoiceConfiguration? conf = null) =>
        ToSsmlContent([str], speakerName, conf);


    private static IReadOnlyCollection<SsmlContent> ToSsmlContent(this IEnumerable<string> strings, string? speakerName, TextToSpeechVoiceConfiguration? conf = null) => strings
        .Select((str, indx) => new SsmlLine(
            SsmlText: str, 
            Configuration: conf,
            SpeakerNameAnnounce: indx == 0 ? speakerName : null
         ))
        .ToList();



    private record VoiceAndConfig(TextToSpeechVoiceDefinition Voice, TextToSpeechVoiceConfiguration? Config);



    private static readonly Regex SentenceSplitRegex = new Regex(@"[^\.\n]+(\.)[^\.\n]+");

    private static IReadOnlyCollection<string> SplitBySentenceBreak(this string str)
    {
        var returnee = new List<string>();
        var startIndex = 0;
        foreach (var match in SentenceSplitRegex.Matches(str).ToList().Select(_ => _.Groups[1]))
        {
            returnee.Add(str.Substring(startIndex, match.Index - startIndex));
            startIndex = match.Index + 1;
        }
        returnee.Add(str.Substring(startIndex));
        return returnee;
    }







    private static readonly Regex UnAllowedCharactersRegex = new Regex("[^A-Z0-9 ]", RegexOptions.IgnoreCase);

    private static string SemanticId(this BookChapterContent cont, BookChapter chapter, string? personDistinguisher) => 
        ($"{chapter.ChapterDateToUse.ToString("yyyy-MM-dd")}{chapter.ChapterOrderToUse}" + (cont switch
        {
            BookCharacterLine lin => (lin.Character.CharacterFormatKey + lin.LineParts.Select(_ => _.PartText).MakeString(" ")).MaxChars() + (personDistinguisher ?? ""),
            BookCharacterStoryTime st => ("Story " +  st.Title).MaxChars(),
            BookContextBreak ct => "CTXBRK" + Guid.NewGuid(),
            BookNarration narr => narr.NarrationContent.MaxChars(),
            BookNarrationList lis => lis.Items.MakeString(" ").MaxChars(),
            BookSinging sin => sin.LinesSong.MakeString(" ").MaxChars() + (personDistinguisher ?? ""),
            _ => cont.GetType().Name
        })).Pipe(str => UnAllowedCharactersRegex.Replace(str, ""));

    private static string MaxChars(this string str, int toTake = 30) => str.Take(toTake).MakeString("");




}
