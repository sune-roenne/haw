using ThousandAcreWoods.Language.Extensions;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
public abstract record SsmlGenerationToken
{
    public virtual bool IsOpen() => false;
    public virtual bool IsOpen<TElem>() where TElem : SsmlGenerationElement => false;

    public virtual bool IsClose() => false;
    public virtual bool IsClose<TElem>() where TElem : SsmlGenerationElement => false;

    public virtual bool IsContent() => false;
    public virtual bool IsContent<TElem>() where TElem : SsmlGenerationElement => false;

    public static SsmlGenerationOpenToken<TElem> Open<TElem>(TElem elem) where TElem : SsmlGenerationElement => new SsmlGenerationOpenToken<TElem>(elem);
    public static SsmlGenerationCloseToken<TElem> Close<TElem>(TElem elem) where TElem : SsmlGenerationElement => new SsmlGenerationCloseToken<TElem>(elem);
    public static SsmlGenerationContentToken<TElem> Content<TElem>(TElem elem) where TElem : SsmlGenerationElement => new SsmlGenerationContentToken<TElem>(elem);
    public static SsmlGenerationCompleteToken<TElem> Complete<TElem>(TElem elem) where TElem : SsmlGenerationElement => new SsmlGenerationCompleteToken<TElem>(elem);

    protected string Indentation(int level) => Enumerable.Range(0, level)
        .Select(_ => "  ")
        .MakeString("");

}

public record SsmlGenerationOpenToken<TElem>(
    TElem Element
    ) : SsmlGenerationToken where TElem : SsmlGenerationElement
{
    public override bool IsOpen() => true;
    public override bool IsOpen<TTest>() => typeof(TTest) == typeof(TElem);

    public override string ToString() => Indentation(Element.NodeLevel) +  Element.OpenTag;

}

public record SsmlGenerationCloseToken<TElem>(
    TElem Element
    ) : SsmlGenerationToken where TElem : SsmlGenerationElement
{
    public override bool IsClose() => true;
    public override bool IsClose<TTest>() => typeof(TTest) == typeof(TElem);

    public override string ToString() => Indentation(Element.NodeLevel) + Element.CloseTag;

}

public record SsmlGenerationContentToken<TElem>(
    TElem Element
    ) : SsmlGenerationToken where TElem : SsmlGenerationElement
{
    public override bool IsContent() => true;
    public override bool IsContent<TTest>() => typeof(TTest) == typeof(TElem);

    public override string ToString() => Indentation(Element.NodeLevel) + (Element.Content ?? "");

}
public record SsmlGenerationCompleteToken<TElem>(
    TElem Element
    ) : SsmlGenerationToken where TElem : SsmlGenerationElement
{
    public override string ToString() => $"{Indentation(Element.NodeLevel)}{Element.OpenTag}{Element.Content ?? ""}{Element.CloseTag}";

}
