namespace ThousandAcreWoods.UI.Components.TextUtilities;

public record FontSettings(
    string? Family = null,
    int? Size = null,
    string? Color = null,
    string? ShadowColor = null,
    FontSettings? Defaults = null
    )
{
    public string? FontFamily => Family ?? Defaults?.FontFamily;
    public string TheFontFamily => FontFamily!;
    public int? FontSize => Size ?? Defaults?.FontSize;
    public int TheFontSize => FontSize!.Value;
    public string? FontColor => Color ?? Defaults?.FontColor;
    public string TheFontColor => FontColor!;

    public string? FontShadowColor => ShadowColor ?? Defaults?.ShadowColor;
    public string TheFontShadowColor => FontShadowColor!;

    public static FontSettings operator +(FontSettings? settings, FontSettings defaults) => settings == null ? defaults : settings with { Defaults = defaults };


}
