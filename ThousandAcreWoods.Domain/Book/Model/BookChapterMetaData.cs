using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookChapterMetaData(
    string? ChapterTitle = null,
    DateTime? ChapterDate = null,
    string? StoryLineKey = null,
    IReadOnlyDictionary<string, string>? Aliases = null,
    string? ChapterOrder = null
    );
