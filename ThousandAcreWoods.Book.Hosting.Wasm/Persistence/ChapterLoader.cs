using System.Net.Http;
using System.Net.Http.Json;
using ThousandAcreWoods.Book.Hosting.Wasm.Model;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Persistence;

public class ChapterLoader : IChapterLoader
{
    private readonly IServiceScopeFactory _scopeFactory;
    private SiteChapter[] _chapters = [];
    private SiteAboutTheAuthor? _aboutTheAuthor;
    private SemaphoreSlim _loadLock = new SemaphoreSlim(1,1);

    public ChapterLoader(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }


    public async Task<IReadOnlyCollection<SiteChapter>> LoadChapters()
    {
        if(!_chapters.Any())
        {
            _chapters = (await LoadChaptersFromStorage()).ToArray();
        }
        return _chapters;
    }

    public async Task<SiteChapter> LoadChapter(int chapterNo)
    {
        if (!_chapters.Any())
            _chapters = (await LoadChaptersFromStorage()).ToArray();
        return _chapters[chapterNo];
    }

    private async Task<IReadOnlyCollection<SiteChapter>> LoadChaptersFromStorage()
    {
        await _loadLock.WaitAsync();
        try
        {
            if (_chapters.Any())
                return _chapters;
            using var scope = _scopeFactory.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            var filesOverview = (await httpClient.GetFromJsonAsync<SiteFileOverview>($"{SitePersistenceConstants.SiteDataDirectory}/{SitePersistenceConstants.SiteFileOverviewFileName}"))!;
            var fileLoadTasks = filesOverview.ChapterFiles
                .Select(async _ => await httpClient.GetFromJsonAsync<SiteChapter>($"{SitePersistenceConstants.SiteDataDirectory}/{filesOverview.Version}/{_}"))
                .ToList();
            var chapters = (await Task.WhenAll(fileLoadTasks))
                .Select(_ => _!)
                .OrderBy(_ => (_.ChapterDate, _.ChapterOrder))
                .ToList();
            return chapters;
        }
        finally
        {
            _loadLock.Release(); 
        }
    }

    public async Task<SiteAboutTheAuthor> LoadAboutTheAuthor()
    {
        if(_aboutTheAuthor != null)
            return _aboutTheAuthor;
        using var scope = _scopeFactory.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
        httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        var filesOverview = (await httpClient.GetFromJsonAsync<SiteFileOverview>($"{SitePersistenceConstants.SiteDataDirectory}/{SitePersistenceConstants.SiteFileOverviewFileName}"))!;
        var loaded = await httpClient.GetFromJsonAsync<SiteAboutTheAuthor>($"{SitePersistenceConstants.SiteDataDirectory}/{filesOverview.Version}/{filesOverview.AboutTheAuthorFileName}");
        _aboutTheAuthor = loaded!;
        return _aboutTheAuthor;

    }
}
