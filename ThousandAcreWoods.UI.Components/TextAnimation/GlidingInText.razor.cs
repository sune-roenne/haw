using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using ThousandAcreWoods.UI.Components.Util;

namespace ThousandAcreWoods.UI.Components.TextAnimation;

public partial class GlidingInText : AnimatedTextComponent
{

    protected override string DeadPartCssRules => $@"
            transform: translate(-200px, -20px) rotate(30deg) scale(.1);
            opacity: 0;";

    protected override string AlivePartCssRules => $@"
            transform: translate(0) rotate(0) scale(1);
            opacity: 1;";



    protected override AnimatedTextTimingFunction TimingFunctionToUse() => TimingFunction ?? AnimatedTextTimingFunction.EaseInOut;

}
