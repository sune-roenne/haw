using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;

namespace ThousandAcreWoods.UI.Components.Common;

public record FontSettings(
    string? Family = null,
    int? Size = null,
    ColorSpecification? Color = null,
    ColorSpecification? ShadowColor = null,
    FontSettings? Defaults = null,
    bool IsItalic = false,
    string? RgbColorString = null,
    string? HexColorString = null,
    string? RgbShadowColorString = null,
    string? HexShadowColorString = null

    )
{
    public string? FontFamily => Family ?? Defaults?.FontFamily;
    public string TheFontFamily => FontFamily!;
    public int? FontSize => Size ?? Defaults?.FontSize;
    public int TheFontSize => FontSize!.Value;

    private ColorSpecification? EvalFontColor => Color ?? 
        ((ColorSpecification?) RgbColorString.PipeOpt(_ => new RgbColorSpecification(_))) ?? 
        HexColorString.PipeOpt(_ => new HexColorSpecification(_));

    private ColorSpecification? EvalShadowColor => ShadowColor??
        ((ColorSpecification?)RgbShadowColorString.PipeOpt(_ => new RgbColorSpecification(_))) ??
        HexShadowColorString.PipeOpt(_ => new HexColorSpecification(_));



    public ColorSpecification? FontColor => EvalFontColor ?? Defaults?.EvalFontColor;
    public ColorSpecification TheFontColor => FontColor!;

    public ColorSpecification? FontShadowColor => EvalShadowColor ?? Defaults?.EvalShadowColor;
    public ColorSpecification TheFontShadowColor => FontShadowColor!;

    public static FontSettings operator +(FontSettings? settings, FontSettings defaults) => settings == null ? defaults : settings with
    {
        Defaults = defaults
    };

    public MarkupString ToCssRules(string separator = " ") => new MarkupString(
        new List<(string, string?)>
        {
            ("font-family", FontFamily),
            ("font-size", FontSize?.Pipe(siz => $"{siz}pt")),
            ("color", FontColor?.AsCssString),
            ("font-style", IsItalic ? "italic" : "normal")
        }.Where(_ => _.Item2 != null)
        .Select(_ => $"{_.Item1}:{_.Item2};")
        .MakeString(separator)
        );



}
