using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.SVGTools.Model;
public record PathElement(
    IReadOnlyCollection<PathLine> Lines,
    DrawingAttributes? Attributes = null
    ) : SvgElement();
