﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.UI.Components.TextAnimation;
public enum AnimatedTextTimingFunction
{
    Linear = 0,
    Ease = 10,
    EaseIn = 11,
    EaseOut = 12,
    EaseInOut = 13,
    Accelerating = 20,
    AcceleratingBreakEnd = 21,
    CubicBezier = 50
}
