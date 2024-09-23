using ThousandAcreWoods.Language.Extensions;
using System.Text.RegularExpressions;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech;
public static class TextToSpeechStyleAnalyzer
{

    public static string? StyleFor(this BookCharacterLinePart linePart) => linePart.Description.PipeOpt(desc => desc.StyleFor());



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


}
