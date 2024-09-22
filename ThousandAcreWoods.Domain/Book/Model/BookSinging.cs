using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookSinging(
    BookCharacter Character,
    IReadOnlyCollection<string> LinesSong
    ) : BookChapterContent
{
}
