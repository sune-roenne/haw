namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;

public record SiteFileOverview(
    string Version,
    IReadOnlyCollection<string> ChapterFiles,
    string AboutTheAuthorFileName
    )
{
}
