using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookCharacterLine(
    BookCharacter Character,
    IReadOnlyCollection<BookCharacterLinePart> LineParts,
    bool IsThought = false
    ) : BookChapterContent;
