using ThousandAcreWoods.Book.Hosting.Wasm.Model;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Persistence;

public interface IChapterLoader
{
    Task<IReadOnlyCollection<SiteChapter>> LoadChapters();

    Task<SiteChapter> LoadChapter(int chapterNo);
    Task<SiteAboutTheAuthor> LoadAboutTheAuthor();
}
