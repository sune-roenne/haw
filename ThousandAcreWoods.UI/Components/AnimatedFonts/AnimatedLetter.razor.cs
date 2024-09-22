using Microsoft.AspNetCore.Components;
using System.Text;
using ThousandAcreWoods.UI.Util;

namespace ThousandAcreWoods.UI.Components.AnimatedFonts;

public abstract partial class AnimatedLetter
{
    private const decimal MillisecondsPerPixel = 200m / 320m;

    private readonly IReadOnlyCollection<AnimatedLetterPath> _paths;
    private readonly ElementId _elementId;

    protected AnimatedLetter(IReadOnlyCollection<AnimatedLetterPath> paths, string letter)
    {
        _paths = paths;
        _elementId = ElementId.For($"taw-animated-letter-{letter}");
    }

    [Parameter]
    public int Height { get; set; }
    protected virtual decimal HeightToWidthProportion => 2m;
    public int Width => (int)(Height / HeightToWidthProportion);
    protected virtual int ViewBoxWidth => 100;
    protected virtual int ViewBoxHeight => 200;

    public int MillisecondsToRender => (int) _paths
        .Sum(_ => _.PathLength * MillisecondsPerPixel);
        

    public IReadOnlyCollection<AnimatedLetterPath> Paths => _paths;

    [Parameter]
    public long InitialDelayInMilliseconds { get; set; } = 0;

    private string CssRules()
    {
        var (cssStyle, animationStyle) = (new StringBuilder(), new StringBuilder());
        var lengths = _paths.Select(_ => _.PathLength).ToList();
        var delay = InitialDelayInMilliseconds;
        foreach(var (length,indx) in lengths.Select((l,indx) => (l, indx)))
        {
            var millisecondsToAnimate = (int) (length * MillisecondsPerPixel);
            cssStyle.Append(_elementId.CssRule(indx.ToString(), [
                ("stroke-linecap", "round"),
                ("stroke-linejoin", "round"),

                ("stroke-dasharray", length.ToString()), 

                ("stroke-dashoffset", length.ToString()),
                ("animation", $"{_elementId.IdFor(indx.ToString())} {millisecondsToAnimate}ms linear {delay}ms forwards")
                ]));
            animationStyle.Append(_elementId.CssAnimationRule(indx.ToString(),
                [
//                    (0, [("stroke-dashoffset", length.ToString())]),
                    (100, [("stroke-dashoffset", "0")]),

                ]));
            delay += (millisecondsToAnimate + 50);
        }
        cssStyle.AppendLine("\r\n" + animationStyle.ToString());

        return cssStyle.ToString();
    }



        

}
