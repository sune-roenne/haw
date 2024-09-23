using ThousandAcreWoods.Language.Extensions;
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
