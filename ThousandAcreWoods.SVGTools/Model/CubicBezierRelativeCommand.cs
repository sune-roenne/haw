using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.SVGTools.Model;
public record CubicBezierRelativeCommand(
    Point StartControlPointOffset,
    Point EndControlPointOffset,
    Point EndPointOffset
    ) : PathCommand();
