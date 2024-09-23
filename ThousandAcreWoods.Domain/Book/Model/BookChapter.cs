using ThousandAcreWoods.Domain.Util;
using ThousandAcreWoods.Language.Extensions;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookChapter(
    DateTime ChapterDate,
    string ChapterName,
    IReadOnlyCollection<BookChapterContent> ChapterContents,
    BookChapterMetaData? MetaData,
    string ChapterOrder = ""
    )
{
    public DateTime ChapterDateToUse = MetaData?.ChapterDate ?? ChapterDate;
    public string ChapterTitleToUse = MetaData?.ChapterTitle ?? ChapterName;
    public string ChapterOrderToUse = MetaData?.ChapterOrder ?? ChapterOrder;
    public IReadOnlyDictionary<string, string> ChapterAliasesToUse = (MetaData?.Aliases ?? new Dictionary<string, string>())
        .ToDictionarySafe(_ => _.Key, _ => _.Value);

    public string ChapterKey => $"{ChapterDateToUse.ToString("yyyy-MM-dd")}{ChapterOrderToUse}-{ChapterTitleToUse.ToLower().Trim()}";

    public string ShaHash(int order) => ChapterContents
        .Select(_ => _.ShaHash())
        .AggregateHash(order.ToString(), _ => _);

    public IReadOnlyCollection<BookCharacter> AllCharacters => ChapterContents
        .Select(cont => cont switch
        {
            BookCharacterLine lin => (BookCharacter?)lin.Character,
            BookCharacterStoryTime st => st.Character,
            BookSinging sin => sin.Character,
            _ => null
        })
        .Where(_ => _ != null)
        .Select(_ => _!)
        .DistinctBy(_ => (_.CharacterName, _.CharacterInfo?.Character))
        .ToList();


}
