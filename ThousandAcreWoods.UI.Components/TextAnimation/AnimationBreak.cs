using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.UI.Components.TextAnimation;
public record AnimationBreak(
    decimal ProgressInPercent,
    IReadOnlyCollection<(string CssAttribute, object? CssValue)> AttributeValues
    );
