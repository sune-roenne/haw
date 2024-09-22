using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.UI.Components.TextAnimation;
public class SlamInText : AnimatedTextComponent
{
    protected override string DeadPartCssRules => $@"
        opacity: 0;
        transform: scale3d(10,10,10);";

    protected override string AlivePartCssRules => $@"
        opacity: 1;
        transform: scale3d(1,1,1)";

    protected override AnimatedTextTimingFunction TimingFunctionToUse() => TimingFunction ?? AnimatedTextTimingFunction.Accelerating;


}
