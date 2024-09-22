using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookRelease(
    IReadOnlyCollection<BookChapter> Chapters,
    IReadOnlyDictionary<string, BookCharacterInfo> CharacterInfos,
    IReadOnlyDictionary<string, BookStoryLine> StoryLines,
    string Author,
    string Version,
    DateTime LastModified,
    BookAboutTheAuthor AboutTheAuthor
    )
{

}
