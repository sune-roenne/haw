using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;
public record SiteChapter(
    DateTime ChapterDate,
    string ChapterName,
    string ChapterFileName,
    IReadOnlyCollection<SiteSerializableChapterContent> SerializableChapterContents,
    int ChapterOrder,
    string ChapterHash,
    SiteChapterVideo Video,
    FontSettings ChapterHeaderFontSettings
    )
{
    public IReadOnlyCollection<SiteChapterContent> ChapterContents = SerializableChapterContents
        .Select(_ => _.ToModel())
        .ToList();


}
