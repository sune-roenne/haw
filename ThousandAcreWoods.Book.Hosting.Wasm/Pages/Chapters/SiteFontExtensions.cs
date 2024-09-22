using ThousandAcreWoods.Book.Hosting.Wasm.Configuration;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public static class SiteFontExtensions
{

    private static readonly IDictionary<string, Func<FontSettings, FontSettings>> _fontCalibrations = new Dictionary<string, Func<FontSettings, FontSettings>>
    {
        { SiteRenderingConstants.FontTradeWinds, _ => _ with { Size = _.Size} },
        { SiteRenderingConstants.FontShadowsIntoLight, _ => _ with { Size = _.Size } },
        { SiteRenderingConstants.FontInkFreeWeb, _ => _ with { Size = _.Size } },
        { SiteRenderingConstants.FontGloriaHalleluja, _ => _ with { Size = _.Size} }
    };

    public static FontSettings Calibrate(this FontSettings fontSettings) =>
        _fontCalibrations.TryGetValue(fontSettings.Family ?? "", out var func) ? func(fontSettings) : fontSettings;



}
