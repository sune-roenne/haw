using ThousandAcreWoods.Language.Extensions;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
public abstract record SsmlGenerationElement(int NodeLevel)
{
    public abstract string OpenTag { get; }
    public abstract string CloseTag { get; }

    public virtual bool AutoClose => false;
    public virtual string? Content => null;

}

public record SsmlGenerationRootElement() : SsmlGenerationElement(NodeLevel: 0)
{
    public override string OpenTag => $@"<speak xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"" version=""1.0"" xml:lang=""en-US"">";
    public override string CloseTag => "</speak>";
}

public record SsmlGenerationVoiceElement(
    TextToSpeechVoiceDefinition Voice
    ) : SsmlGenerationElement(NodeLevel: 1)
{
    public override string OpenTag => $@"<voice name=""{Voice.ShortName}"">";
    public override string CloseTag => "</voice>";
}

public record SsmlGenerationExpressAsElement(
    string Style,
    decimal? StyleDegree,
    string? Role
    ) : SsmlGenerationElement(NodeLevel: 2)
{
    public override string OpenTag => new List<(string Name, string? Value)>{
        ("style", Style),
        ("styledegree", NormalizeStyleIntensity(StyleDegree).ToUsFormatString()),
        ("role", Role)
    }.Where(_ => _.Value != null)
     .Select(_ => $"{_.Name}=\"{_.Value}\"")
     .MakeString(" ", " ", " ")
     .Pipe(attrs => $"<mstts:express-as{attrs}>");

    public override string CloseTag => "</mstts:express-as>";

    private static decimal? NormalizeStyleIntensity(decimal? intensity) => intensity switch
    {
        null => null,
        <= SsmlConstants.VoiceStyleIntensity.Min => SsmlConstants.VoiceStyleIntensity.Min,
        >= SsmlConstants.VoiceStyleIntensity.Max => SsmlConstants.VoiceStyleIntensity.Max,
        _ => intensity
    };

    public bool IsSameAs(SsmlGenerationExpressAsElement other) =>
        OpenTag == other.OpenTag;

}

public record SsmlGenerationProsodyElement(
    int NodeLevel,
    TextToSpeechVoiceConfiguration Configuration
    ) : SsmlGenerationElement(NodeLevel)
{
    public override string OpenTag => new List<(string Name, string? Value)>
        {
            ("contour", Configuration.ContourChanges?.Select(p => $"({p.ProgressInPercent.ToUsFormatString()},{p.ChangeInHertz.AsPercentChange()})")?.MakeString(" ")),
            ("pitch", Configuration.PitchInHertz?.Pipe(_ => $"{_}Hz") ?? Configuration.PitchChangeInPercent?.Pipe(_ => _.AsPercentChange())),
            ("rate", Configuration.RateInPercent.AsPercentChange())
        }.Where(_ => _.Value != null)
         .Select(_ => _.Value.OptAttr(_.Name))
         .MakeString(" ")
         .Pipe(attrString => $@"<prosody{attrString}>"
         );

    public override string CloseTag => "</prosody>";

    public bool IsSameAs(SsmlGenerationProsodyElement other) =>
        OpenTag == other.OpenTag;
}



public record SsmlGenerationParagraphElement(int NodeLevel) : SsmlGenerationElement(NodeLevel)
{
    public static readonly IReadOnlySet<Type> PossibleParents = new HashSet<Type> {
        typeof(SsmlGenerationExpressAsElement),
        typeof(SsmlGenerationVoiceElement)
    };

    public override string OpenTag => "<p>";

    public override string CloseTag => "</p>";
}


public record SsmlGenerationSentenceElement(int NodeLevel, string Sentence) : SsmlGenerationElement(NodeLevel)
{
    public override string OpenTag => "<s>";

    public override string CloseTag => "</s>";

    public override bool AutoClose => true;
    public override string? Content => Sentence;
}

public record SsmlGenerationPauseElement(int NodeLevel, int PauseInMilliseconds) : SsmlGenerationElement(NodeLevel)
{
    public static string BreakTagOf(int millis) => $"<break time=\"{millis}ms\"/>";

    public override string OpenTag => $"<s>{BreakTagOf(PauseInMilliseconds)}</s>";
    public override string? Content => "";
    public override string CloseTag => "";
    public override bool AutoClose => true;


}

