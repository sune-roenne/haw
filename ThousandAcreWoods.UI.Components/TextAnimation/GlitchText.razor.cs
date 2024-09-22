using Microsoft.AspNetCore.Components;
using System.Text;
using ThousandAcreWoods.UI.Components.Common;
using ThousandAcreWoods.UI.Components.Util;

namespace ThousandAcreWoods.UI.Components.TextAnimation;

public partial class GlitchText
{

    private readonly long _id = UniqueId.NextId();
    private readonly Random _random = new Random();

    private static readonly FontSettings FontDefaults = new FontSettings(Family: "Algerian", Size: 150);


    [Parameter]
    public string BackgroundColor { get; set; } = "black";

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public string Subtitle { get; set; } = "";

    [Parameter]
    public FontSettings? FontSettings { get; set; }
    private FontSettings TheFontSettings => FontSettings + FontDefaults;





    private string GlitchId => $"taw-text-glitch-text-{_id}";

    private string ClassId(string suffix) => $"{GlitchId}-{suffix}";


    private string GenerateGlitchAnimationSteps()
    {
        var returnee = new StringBuilder();
        for (var i = 0; i < 30; i++)
        {
            var top = _random.Next(0, 100);
            var bottom = _random.Next(0, 101 - top);
            returnee.AppendLine($"{(((double)i) / 30.0).AsCssPercent()} {{ clip-path: inset({top}px 0 {bottom}px 0);}}");
        }
        return returnee.ToString();

    }


}
