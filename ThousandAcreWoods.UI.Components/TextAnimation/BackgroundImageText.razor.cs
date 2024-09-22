using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.UI.Components.Common;
using ThousandAcreWoods.UI.Components.Util;

namespace ThousandAcreWoods.UI.Components.TextAnimation;

public partial class BackgroundImageText
{

    private readonly long _id = UniqueId.NextId();

    private static readonly FontSettings FontDefaults = new FontSettings(Family: "Bernard MT", Size: 50);

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public string ImageFile { get; set; } = TextUtilitiesConstants.Images.ColorSmokeBackground;

    [Parameter]
    public FontSettings? FontSettings { get; set; }

    private FontSettings TheFontSettings => FontSettings + FontDefaults;





}
