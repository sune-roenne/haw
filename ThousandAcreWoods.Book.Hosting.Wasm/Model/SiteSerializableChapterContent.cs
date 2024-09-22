using static ThousandAcreWoods.Book.Hosting.Wasm.Model.SiteCharacterStoryTime;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;

public record SiteSerializableChapterContent(
    SiteSerializableChapterContentType ContentType,
    string? Title = null,
    SiteCharacter? Character = null,
    IReadOnlyCollection<SiteCharacterLinePart>? LineParts = null,
    bool? IsThought = null,
    IReadOnlyCollection<SiteSerilizableCharacterStoryTimeParagraph>? Paragraphs = null,
    string? Text = null,
    IReadOnlyCollection<string>? Items = null,
    bool? IsNumbered = null
    )

{
    public SiteChapterContent ToModel() => ContentType switch {
        SiteSerializableChapterContentType.Section => new SiteChapterSection(Title: Title!),
        SiteSerializableChapterContentType.CharacterLine => new SiteCharacterLine(Character: Character!, LineParts: LineParts!, IsThought: IsThought!.Value),
        SiteSerializableChapterContentType.CharacterStoryTime => new SiteCharacterStoryTime(Character: Character!, Title: Title!, Paragraphs: Paragraphs!.Select(_ => _.ToModel()).ToList()),
        SiteSerializableChapterContentType.ContextBreak => new SiteContextBreak(),
        SiteSerializableChapterContentType.Narration => new SiteNarration(NarrationContent: Text!),
        SiteSerializableChapterContentType.NarrationList => new SiteNarrationList(Items: Items!, IsNumbered: IsNumbered!.Value),
        SiteSerializableChapterContentType.Singing => new SiteSinging(Character: Character!, LinesSong: Items!),
        _ => throw new Exception("What????!")
    };
}

public static class SiteSerializableChapterContentExtensions
{
    public static SiteSerilizableCharacterStoryTimeParagraph ToSerializableModel(this SiteCharacterStoryTimeParagraph par) => par switch
    {
        SiteCharacterStoryTimeItemListParagraph lis => new SiteSerilizableCharacterStoryTimeParagraph(Items: lis.Items),
        SiteCharacterStoryTimeTextParagraph tex => new SiteSerilizableCharacterStoryTimeParagraph(Text: tex.Text),
        _ => throw new Exception("Lolleren")
    };


    public static SiteSerializableChapterContent ToSerializableModel(this SiteChapterContent content) => content switch
    {
        SiteChapterSection sec => new SiteSerializableChapterContent(ContentType: SiteSerializableChapterContentType.Section, Title: sec.Title),
        SiteCharacterLine lin => new SiteSerializableChapterContent(ContentType: SiteSerializableChapterContentType.CharacterLine, Character: lin.Character, LineParts: lin.LineParts, IsThought: lin.IsThought),
        SiteCharacterStoryTime sto => new SiteSerializableChapterContent(ContentType: SiteSerializableChapterContentType.CharacterStoryTime, Title: sto.Title, Character: sto.Character, Paragraphs: sto.Paragraphs.Select(_ => _.ToSerializableModel()).ToList()),
        SiteContextBreak br => new SiteSerializableChapterContent(ContentType: SiteSerializableChapterContentType.ContextBreak),
        SiteNarration narr => new SiteSerializableChapterContent(ContentType: SiteSerializableChapterContentType.Narration, Text: narr.NarrationContent),
        SiteNarrationList lis => new SiteSerializableChapterContent(ContentType: SiteSerializableChapterContentType.NarrationList, Items: lis.Items, IsNumbered: lis.IsNumbered),
        SiteSinging sin => new SiteSerializableChapterContent(ContentType: SiteSerializableChapterContentType.Singing, Character: sin.Character, Items: sin.LinesSong),
        _ => throw new Exception("No ways")
    };
}


public record SiteSerilizableCharacterStoryTimeParagraph(
    IReadOnlyCollection<string>? Items = null,
    string? Text = null
    )
{

    public SiteCharacterStoryTimeParagraph ToModel() => Items != null ? new SiteCharacterStoryTimeItemListParagraph(Items: Items) : new SiteCharacterStoryTimeTextParagraph(Text: Text!);

}



public enum SiteSerializableChapterContentType
{
    Section = 1,
    CharacterLine = 2,
    CharacterStoryTime = 3,
    ContextBreak = 4,
    Narration = 5,
    NarrationList = 6,
    Singing = 7
}
