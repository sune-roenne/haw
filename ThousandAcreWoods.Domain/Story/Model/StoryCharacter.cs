using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Story.Model;
public record StoryCharacter(
    string Id,
    string Name,
    string ColorLight,
    string? ColorDark = null
    );
