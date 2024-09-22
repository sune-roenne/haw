using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using System;
using ThousandAcreWoods.UI.Components.Util;

namespace ThousandAcreWoods.UI.Components.TextAnimation;

public partial class FlashOnOffText : AnimatedTextComponent
{

    protected override string DeadPartCssRules => $@"
           filter:blur(140px);
           opacity: 0;";

    protected override string AlivePartCssRules => $@"
           filter:blur(0px);
           opacity: 1;";

    protected override AnimatedTextTimingFunction TimingFunctionToUse() => AnimatedTextTimingFunction.EaseOut;



}
