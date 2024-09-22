using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using System.Text;
using ThousandAcreWoods.UI.Components.Util;
using static ThousandAcreWoods.UI.Components.Common.CommonUtil;

namespace ThousandAcreWoods.UI.Components.TextAnimation;

public class RotateInText : AnimatedTextComponent
{
    private const string Transform = "transform";
    private static UnquotedString TransformValue(int x, int y, int deg) => new UnquotedString($"{Translate(x, y)} {Rotate(deg)}");
    private static string Translate(int x, int y) => $"translate({x}px, {y}px)";
    private static string Rotate(int deg) => $"rotate({deg}deg)";


    protected override IReadOnlyCollection<AnimationBreak>? AnimationBreaks => [
        new AnimationBreak(0, [(Transform, TransformValue(-40, 100, 90)), ("opacity", 0)]),
        new AnimationBreak(10, [(Transform, TransformValue(-40, 50, 180)), ("opacity", 0.2)]),
        new AnimationBreak(20, [(Transform, TransformValue(-32, 0, 270))]),
        new AnimationBreak(30, [(Transform, TransformValue(-24, -30, 0))]),
        new AnimationBreak(40, [(Transform, TransformValue(-20, -60, 90))]),
        new AnimationBreak(50, [(Transform, TransformValue(-16, -70, 175))]),
        new AnimationBreak(60, [(Transform, TransformValue(-8, -60, 250))]),
        new AnimationBreak(70, [(Transform, TransformValue(-4, -40, 310))]),
        new AnimationBreak(100, [(Transform, TransformValue(0, 0, 0)), ("opacity", 1)])

        ];


    protected override AnimatedTextTimingFunction TimingFunctionToUse() => TimingFunction ?? AnimatedTextTimingFunction.Linear;
}
