using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.Application.Book.Persistence;
using ThousandAcreWoods.Domain.Book.Model;
using ThousandAcreWoods.LocalStorage.Book.StorageModel;
using ThousandAcreWoods.LocalStorage.Configuration;

namespace ThousandAcreWoods.LocalStorage.Book;
internal class LocalStorageBookRepository : IBookRepository
{
    private const string CharacterMapFileName = "characters.json";
    private const string StoryLineMapFileName = "storylines.json";
    private const string BookReleaseInfoFileName = "book.json";
    private const string AboutTheAuthorFileName = "about-the-author.ata";

    private readonly LocalStorageConfiguration _conf;
    private readonly ILogger<LocalStorageBookRepository> _logger;

    public LocalStorageBookRepository(IOptions<LocalStorageConfiguration> conf, ILogger<LocalStorageBookRepository> logger)
    {
        _conf = conf.Value;
        _logger = logger;
    }

    public async Task<BookRelease> LoadBookFromInput()
    {
        var characterMap = (await Load<BookCharacterInfoMapLso>(CharacterMapFileName)).Characters
            .ToDictionarySafe(_ => _.CharacterKey);
        var storyLines = (await Load<BookStoryLineMapLso>(StoryLineMapFileName))
            .StoryLines.ToDictionarySafe(_ => _.StoryLineKey);
        var bookReleaseInfo = await Load<BookReleaseInfoLso>(BookReleaseInfoFileName);
        var chapters = new List<BookChapter>();
        var lastModification = DateTime.Now.AddDays(-365);
        foreach(var chapFile in Directory.GetFiles(_conf.BookStoryInputFolder).Where(_ => _.ToLower().EndsWith(".story")))
        {
            var lastModTime = File.GetLastWriteTime(chapFile);
            if(lastModTime > lastModification)
                lastModification = lastModTime;
            try
            {
                var parsedChap = await SimpleBookParser.ParseChapter(chapFile, characterMap);
                chapters.Add(parsedChap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"During parsing of file: {chapFile}");
            }
        }
        chapters = chapters
            .OrderBy(_ => (_.ChapterDateToUse, _.ChapterOrderToUse))
            .ToList();
        var aboutTheAuthor = await Path.Combine(_conf.BookStoryInputFolder, AboutTheAuthorFileName)
            .Pipe(SimpleBookParser.ParseAuthorInformation);

        var returnee = new BookRelease(
            Chapters: chapters,
            CharacterInfos: characterMap,
            StoryLines: storyLines,
            Author: bookReleaseInfo.Author,
            Version: bookReleaseInfo.Version,
            LastModified: lastModification,
            AboutTheAuthor: aboutTheAuthor
            );
        return returnee;

    }

    public Task<BookRelease> LoadBookFromOutput()
    {
        throw new NotImplementedException();
    }

    public Task<BookRelease> UpdateOutput(BookRelease fromInput, bool allowOverwritingChanges)
    {
        throw new NotImplementedException();
    }

    private async Task<TRes> Load<TRes>(string fileName) => (await File.ReadAllTextAsync(Path.Combine(_conf.BookStoryInputFolder, fileName)))
        .Pipe(txt => JsonSerializer.Deserialize<TRes>(txt))!;


}
