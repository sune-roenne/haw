using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;
public record SiteSinging(
    SiteCharacter Character,
    IReadOnlyCollection<string> LinesSong
    ) : SiteChapterContent
{
}
