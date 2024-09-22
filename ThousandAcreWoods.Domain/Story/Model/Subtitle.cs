using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Story.Model;
public record Subtitle(
    IReadOnlyCollection<SubtitlePart> Parts,
    bool Emphasize = false
    );
