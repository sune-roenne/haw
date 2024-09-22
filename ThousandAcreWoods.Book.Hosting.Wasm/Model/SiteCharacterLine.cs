using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.UI.Components.TextAnimation;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;
public record SiteCharacterLine(
    SiteCharacter Character,
    IReadOnlyCollection<SiteCharacterLinePart> LineParts,
    bool IsThought = false
    ) : SiteChapterContent;
