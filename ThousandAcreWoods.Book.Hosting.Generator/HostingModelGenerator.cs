using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.Book.Hosting.Generator.Configuration;
using ThousandAcreWoods.Book.Hosting.Wasm.Configuration;
using ThousandAcreWoods.Book.Hosting.Wasm.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;
using ThousandAcreWoods.Domain.Book.Model;
using ThousandAcreWoods.UI.Components.Common;
using ThousandAcreWoods.UI.Components.TextAnimation;

namespace ThousandAcreWoods.Book.Hosting.Generator;
public class HostingModelGenerator : IHostingModelGenerator
{
    private const string AboutTheAuthorFileName = "about-the-author.json";
    private readonly ILogger<HostingModelGenerator> _logger;
    private readonly SiteGeneratorConfiguration _conf;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
    };

    public HostingModelGenerator(ILogger<HostingModelGenerator> logger, IOptions<SiteGeneratorConfiguration> conf)
    {
        _logger = logger;
        _conf = conf.Value;
    }





    public async Task GenerateSiteData(BookRelease book)
    {
        var outputDir = Path.Combine(_conf.GenerationOutputDirectory, book.Version);
        if(Directory.Exists(outputDir))
            Directory.Delete(outputDir, true);
        Directory.CreateDirectory(outputDir);


        var chaptersToWrite = book.Chapters
            .OrderBy(_ => (_.ChapterDateToUse, _.ChapterOrderToUse))
            .Select((_, indx) => _.ToSiteModel(indx, _.MetaData?.StoryLineKey?.PipeOpt(stk =>  book.StoryLines[stk]) ?? book.StoryLines.First().Value))
            .ToList();

        foreach(var chapter in chaptersToWrite)
        {
            var outputFile = Path.Combine(outputDir, chapter.ChapterFileName);
            var jsonFileContent = JsonSerializer.Serialize(chapter, options: _jsonSerializerOptions);
            await File.WriteAllTextAsync(outputFile, jsonFileContent);
            _logger.LogInformation($"Created data file: {outputFile}");
        }

        var writtenFiles = chaptersToWrite
            .Select(_ => _.ChapterFileName)
            .ToList();

        var aboutTheAuthorFileName = Path.Combine(outputDir, AboutTheAuthorFileName);
        var aboutTheAuthorContent = JsonSerializer.Serialize(book.AboutTheAuthor.ToSiteModel(), options: _jsonSerializerOptions);
        await File.WriteAllTextAsync(aboutTheAuthorFileName, aboutTheAuthorContent);

        var siteFileOverview = new SiteFileOverview(
            Version: book.Version,
            ChapterFiles: writtenFiles,
            AboutTheAuthorFileName: AboutTheAuthorFileName
            );
        var overviewJson = JsonSerializer.Serialize(siteFileOverview, options: _jsonSerializerOptions);
        var overviewFileName = Path.Combine(_conf.GenerationOutputDirectory, SitePersistenceConstants.SiteFileOverviewFileName);
        await File.WriteAllTextAsync(overviewFileName, overviewJson);

        _logger.LogInformation($"All done creating data files!");

    }




}


public static class HostingModelGeneratorMappings
{
    public static SiteAboutTheAuthor ToSiteModel(this BookAboutTheAuthor bookAboutTheAuthor) => new SiteAboutTheAuthor(
        bookAboutTheAuthor.Contents.Select(_ => _
            .ToSiteModelFromBase()
            .ToSerializableModel()
        ).ToList()
        );

    public static SiteCharacter ToSiteModel(this BookCharacter character) => new SiteCharacter(
        CharacterKey: character.CharacterKey,
        CharacterName: character.CharacterName,
        Color: character.CharacterInfo?.Color,
        Font: character.CharacterInfo?.SiteFont
        );

    public static SiteStoryLine ToSiteModel(this BookStoryLine bookObj) => new SiteStoryLine(
        StoryLineKey: bookObj.StoryLineKey,
        StoryLine: bookObj.StoryLine,
        StoryLineName: bookObj.StoryLineName,
        Image: bookObj.Image
        );

    public static SiteSinging ToSiteModel(this BookSinging bookObj) => new SiteSinging(
        Character: bookObj.Character.ToSiteModel(),
        LinesSong: bookObj.LinesSong.ToList()
        );

    public static SiteNarrationList ToSiteModel(this BookNarrationList bookObj) => new SiteNarrationList(
        Items: bookObj.Items.ToList(),
        IsNumbered: bookObj.IsNumbered
        );

    public static SiteNarration ToSiteModel(this BookNarration bookObj) => new SiteNarration(
        NarrationContent: bookObj.NarrationContent
        );

    public static SiteContextBreak ToSiteModel(this BookContextBreak bookObj) => new SiteContextBreak();

    public static SiteCharacterStoryTime ToSiteModel(this BookCharacterStoryTime bookObj) => new SiteCharacterStoryTime(
        Character: bookObj.Character.ToSiteModel(),
        Title: bookObj.Title,
        Paragraphs: bookObj.Paragrahs
           .Select(par => par switch
           {
               BookCharacterStoryTime.BookCharacterStoryTimeTextParagraph texPar => (SiteCharacterStoryTime.SiteCharacterStoryTimeParagraph) new SiteCharacterStoryTime.SiteCharacterStoryTimeTextParagraph(texPar.Text),
               BookCharacterStoryTime.BookCharacterStoryTimeItemListParagraph itLisPar => new SiteCharacterStoryTime.SiteCharacterStoryTimeItemListParagraph(itLisPar.Items.ToList()),
               _ => throw new NotImplementedException()
           }).ToList()
        );

    public static SiteCharacterLinePart ToSiteModel(this BookCharacterLinePart bookObj) => new SiteCharacterLinePart(
        PartText: bookObj.PartText,
        Description: bookObj.Description
    );

    public static SiteCharacterLine ToSiteModel(this BookCharacterLine bookObj) => new SiteCharacterLine(
        Character: bookObj.Character.ToSiteModel(),
        LineParts: bookObj.LineParts.Select(_ => _.ToSiteModel()).ToList(),
        IsThought: bookObj.IsThought
    );
    public static SiteChapterSection ToSiteModel(this BookChapterSection bookObj) => new SiteChapterSection(
        Title: bookObj.Title
    );

    public static SiteChapter ToSiteModel(this BookChapter chap, int order, BookStoryLine storyLine) => new SiteChapter(
        ChapterDate: chap.ChapterDate,
        ChapterName: chap.ChapterTitleToUse,
        ChapterFileName: $"{chap.ChapterDateToUse.ToString("yyyy-MM-dd")}{(chap.ChapterOrderToUse.Length > 0 ? ("-" + chap.ChapterOrderToUse) : "")}-{chap.ChapterName.Trim()}.json",
        SerializableChapterContents: chap.ChapterContents
           .Select(_ => _.ToSiteModelFromBase())
           .Select(_ => _.ToSerializableModel())
           .ToList()
        ,
        ChapterOrder: order,
        ChapterHash: chap.ShaHash(order),
        Video: new SiteChapterVideo(
            FileName: storyLine.Video,
            AttributionText: storyLine.Attribution.Text,
            AttributionLink: storyLine.Attribution.Link
            ),
        ChapterHeaderFontSettings: new FontSettings(Family: storyLine.SiteFont, Size: SiteRenderingConstants.DefaultHeaderSize)
        );

    public static SiteChapterContent ToSiteModelFromBase(this BookChapterContent cont) => cont switch
    {
        BookChapterSection sec => (SiteChapterContent)sec.ToSiteModel(),
        BookCharacterLine lin => lin.ToSiteModel(),
        BookCharacterStoryTime st => st.ToSiteModel(),
        BookContextBreak br => br.ToSiteModel(),
        BookNarration narr => narr.ToSiteModel(),
        BookNarrationList lis => lis.ToSiteModel(),
        BookSinging sin => sin.ToSiteModel(),
        _ => throw new NotImplementedException()
    };

}
