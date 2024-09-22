using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using System.Text;
using ThousandAcreWoods.UI.Components.Common;
using ThousandAcreWoods.UI.Components.Util;

namespace ThousandAcreWoods.UI.Components.TextAnimation;

public partial class BlinkingNeonLights
{
    private static readonly FontSettings FontDefaults = new FontSettings(Family: "Curlz MT", Size: 130, Color: new RgbColorSpecification("rgb(255,213,255)"), ShadowColor: new RgbColorSpecification("rgb(212,44,202)"));

    [Parameter]
    public string Text { get; set; }
    [Parameter]
    public IReadOnlyCollection<int> FastBlinkingIndexes { get; set; }

    [Parameter]
    public IReadOnlyCollection<int> SlowBlinkingIndexes { get; set;}

    [Parameter]
    public FontSettings? FontSettings { get; set; }
    private FontSettings TheFontSettings => FontSettings + FontDefaults;

    private readonly long _id = UniqueId.NextId();

    private IReadOnlyCollection<TextBit> _textBits = [];

    protected override Task OnParametersSetAsync()
    {
        SplitText();
        return base.OnParametersSetAsync();
    }

    private MarkupString TextMarkupString => _textBits.Select(bit => bit switch
    {
        _ when bit.IsFast => $"""<span class="taw-text-neon-blink-fast-{_id}">{bit.Text}</span>""",
        _ when bit.IsSlow => $"""<span class="taw-text-neon-blink-slow-{_id}">{bit.Text}</span>""",
        _ => bit.Text
    }).MakeString("").Pipe(_ => new MarkupString(_));


    private void SplitText()
    {
        var split = new List<TextBit>();
        var currentSlow = new StringBuilder();
        var currentFast = new StringBuilder();
        var currentNo = new StringBuilder();
        for (int i = 0; i < Text.Length; i++)
        {
            if (SlowBlinkingIndexes.Contains(i))
            {
                if(currentFast.Length > 0) {
                    split.Add(new TextBit(currentFast.ToString(), IsFast: true));
                    currentFast = new StringBuilder();
                }
                if (currentNo.Length > 0)
                {
                    split.Add(new TextBit(currentNo.ToString()));
                    currentNo = new StringBuilder();
                }
                currentSlow.Append(Text[i]);
            }
            else if (FastBlinkingIndexes.Contains(i))
            {
                if (currentSlow.Length > 0)
                {
                    split.Add(new TextBit(currentSlow.ToString(), IsSlow: true));
                    currentSlow = new StringBuilder();
                }
                if (currentNo.Length > 0)
                {
                    split.Add(new TextBit(currentNo.ToString()));
                    currentNo = new StringBuilder();
                }
                currentFast.Append(Text[i]);

            }
            else
            {
                if (currentSlow.Length > 0)
                {
                    split.Add(new TextBit(currentSlow.ToString(), IsSlow: true));
                    currentSlow = new StringBuilder();
                }
                if (currentFast.Length > 0)
                {
                    split.Add(new TextBit(currentFast.ToString(), IsFast: true));
                    currentFast = new StringBuilder();
                }
                currentNo.Append(Text[i]);
            }
        }
        if (currentSlow.Length > 0)
            split.Add(new TextBit(currentSlow.ToString(), IsSlow: true));
        if (currentFast.Length > 0)
            split.Add(new TextBit(currentFast.ToString(), IsFast: true));
        if(currentNo.Length > 0)
            split.Add(new TextBit(currentNo.ToString()));
        _textBits = split.ToArray();
    }


    private record TextBit(string Text, bool IsSlow = false, bool IsFast = false)
    {
        public bool IsBlinking => IsSlow || IsFast;
    }

}
