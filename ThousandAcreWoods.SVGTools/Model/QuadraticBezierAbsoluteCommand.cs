using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.SVGTools.Model;
public record QuadraticBezierAbsoluteCommand(
    Point ControlPoint,
    Point EndPoint
    ) : PathCommand();
