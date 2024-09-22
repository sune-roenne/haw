using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.SVGTools.Model;
public record DrawingAttributes(
    string? Stroke = null,
    string? StrokeWidth = null,
    string? StrokeOpacity = null,
    string? Fill = null,
    string? FillOpacity = null
    );
