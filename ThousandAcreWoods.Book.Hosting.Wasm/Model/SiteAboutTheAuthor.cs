namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;

public record SiteAboutTheAuthor(
    IReadOnlyCollection<SiteSerializableChapterContent> SerializableContents
    )
{
    public IReadOnlyCollection<SiteChapterContent> Content = SerializableContents
        .Select(_ => _.ToModel())
        .ToList();

};
