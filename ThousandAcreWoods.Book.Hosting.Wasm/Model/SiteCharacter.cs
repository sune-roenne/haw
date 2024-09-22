using NYK.Collections.Extensions;
using ThousandAcreWoods.Book.Hosting.Wasm.Configuration;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;
public record SiteCharacter(
    string CharacterKey,
    string CharacterName,
    string? Color = null,
    string? Font = null,
    int FontSize = SiteRenderingConstants.DefaultFontSize
    )
{
    public FontSettings FontSettings = new FontSettings(Family: Font, Color:  Color.PipeOpt(_ => new RgbColorSpecification(_)), Size: FontSize);

}
