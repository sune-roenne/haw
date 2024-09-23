using ThousandAcreWoods.Language.Extensions;
using System.Reflection;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech;
internal static class TextToSpeechLookups
{
    static TextToSpeechLookups()
    {
        var maleTypes = ExtractVoicesFrom(typeof(SsmlConstants.Voices.Men));
        var femaleTypes = ExtractVoicesFrom(typeof(SsmlConstants.Voices.Women));
        var neutralTypes = ExtractVoicesFrom(typeof(SsmlConstants.Voices.Neutrals));
        VoiceDefinitions = maleTypes
            .Concat(femaleTypes)
            .Concat(neutralTypes)
            .ToDictionarySafe(_ => _.ShortName.ToLower().Trim());
        Styles = typeof(SsmlConstants.VoiceStyles).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(_ => _.FieldType == typeof(string))
            .Select(_ => _.GetValue(null)!.ToString()!)
            .ToDictionarySafe(_ => _.Trim().ToLower());

    }

    private static IReadOnlyCollection<TextToSpeechVoiceDefinition> ExtractVoicesFrom(Type typ)
    {
        var returnee = typ.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(_ => _.FieldType == typeof(TextToSpeechVoiceDefinition))
            .Select(_ => (TextToSpeechVoiceDefinition)_.GetValue(null)!)
            .ToList();
        return returnee;
    }

    private static readonly IReadOnlyDictionary<string, TextToSpeechVoiceDefinition> VoiceDefinitions;
    private static readonly IReadOnlyDictionary<string, string> Styles;

    public static IReadOnlyCollection<TextToSpeechVoiceDefinition> AllVoices => VoiceDefinitions
        .Values
        .OrderBy(_ => (_.Gender, _.ShortName))
        .ToList();

    public static TextToSpeechVoiceDefinition VoiceFor(string shortName) => VoiceDefinitions[shortName.ToLower().Trim()];
    public static string StyleFor(string styleName) => Styles[styleName.ToLower().Trim()];

    public static TextToSpeechVoiceDefinition? VoiceForOpt(string shortName) => 
        VoiceDefinitions.TryGetValue(shortName.ToLower().Trim(), out var voice) ? voice : null;
    public static string? StyleForOpt(string styleName) => 
        Styles.TryGetValue(styleName.ToLower().Trim(), out var voice) ? voice : null;

}
