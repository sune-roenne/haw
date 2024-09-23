using ThousandAcreWoods.Language.Extensions;
using System.Reflection;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.AudioBook.TextToSpeech;
public static class SsmlHashingCalculator
{

    public static string DefaultHash = "This is just a string man!".ToShaHash();

    public static string Hash(this IEnumerable<SsmlEntry> entries) => entries.Count() == 0 ? DefaultHash :
        entries.MakeString("").ToShaHash();
         

    public static string Hash(this SsmlEntry entry) => entry.HashByProps([
        nameof(entry.CharacterKey),
        nameof(entry.Voice),
        nameof(entry.Configuration),
        nameof(entry.Content)

        ]);

    public static string Hash(this SsmlContent cont) => cont switch
    {
        SsmlPause p => p.Hash(),
        SsmlLine l => l.Hash(),
        _ => ""
    };

    public static string Hash(this SsmlPause pause) => pause.HashByProps([nameof(pause.DurationInMilliseconds)]);

    public static string Hash(this SsmlLine entry) => entry.HashByProps([
        nameof(entry.SsmlText),
        nameof(entry.Configuration)
        ]);

    public static string? Hash(this TextToSpeechVoiceConfiguration? conf) => conf.PipeOpt(c => c.HashByProps(
        propertyNames: [
            nameof(c.ContourChanges),
            nameof(c.PitchChangeInPercent),
            nameof(c.PitchInHertz),
            nameof(c.RateInPercent),
            nameof(c.Role),
            nameof(c.Style)
        ], 
        stringer: StringIt
    ));

    public static string HashByProps<T>(this T instance, IEnumerable<string> propertyNames, Func<object?, string?>? stringer = null) where T : class => typeof(T).Pipe(
        typ => instance.HashByProps(
            propertyNames.Select(_ => typ.GetProperty(_)!)
        ));



    public static string HashByProps<T>(this T instance, IEnumerable<PropertyInfo> properties, Func<object?, string?>? stringer = null) where T : class =>
        instance.HashByProps(
            (instance) => properties
                .Select(_ => (_.Name, _.GetValue(instance)?.Pipe(val => stringer?.Invoke(val) ?? StringIt(val))))
            );



    public static string HashByProps<T>(this T instance, Func<T, IEnumerable<(string Key, string? Value)>> valuesExtractor) where T : class =>
        valuesExtractor(instance)
        .Where(_ => _.Value != null)
        .Select(_ => $"{_.Key}={_.Value}")
        .Pipe(
            coll => coll.Count() == 0 ? "" : coll.MakeString(",").ToShaHash()
        );


    private static string? StringIt(this object? value) => value switch
    {
        null => null,
        decimal decVal => "" + ((int)(decVal * 100_000)),
        string str => str,
        TextToSpeechVoiceDefinition voice => voice.ShortName,
        TextToSpeechContourChange chan => $"{chan.ProgressInPercent.StringIt()}{chan.ChangeInHertz.StringIt()}",
        TextToSpeechVoiceConfiguration conf => $"{conf.ContourChanges.StringIt()}{conf.RateInPercent.StringIt()}{conf.PitchChangeInPercent.StringIt()}{conf.Style.StringIt()}{conf.Role.StringIt()}",
        _ when value.GetType().IsAssignableTo(typeof(IEnumerable<SsmlContent>)) => ((IEnumerable<SsmlContent>) value)
              .Select(_ => _.Hash())
              .Pipe(coll => coll.Count() == 0 ? "" : coll.MakeString("").ToShaHash()),
        _ => value!.ToString()
    };


}
