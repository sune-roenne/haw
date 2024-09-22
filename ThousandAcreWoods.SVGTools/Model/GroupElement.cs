using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.SVGTools.Model;
public record GroupElement(
    IReadOnlyCollection<PathElement> Paths,
    IReadOnlyCollection<GroupElement>? ContainedGroups = null,
    DrawingAttributes? Attributes = null
    ) : SvgElement();
