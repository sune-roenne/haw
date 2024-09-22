using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThousandAcreWoods.LocalStorage;
using ThousandAcreWoods.Application.Book.Persistence;
using ThousandAcreWoods.AudioBook;
using Microsoft.Extensions.Logging;
using ThousandAcreWoods.AudioBook.TextToSpeech.Audio;
using NLog.Extensions.Logging;
using ThousandAcreWoods.AudioBook.TextToSpeech.Helpers;

// Configure
var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.local.json", optional: true)
    .Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(logBuilder =>{
    logBuilder.ClearProviders();
    logBuilder.SetMinimumLevel(LogLevel.Debug);
    logBuilder.AddNLog(conf);
    //logBuilder.AddConsole();
});
serviceCollection
    .AddLocalStorageConfiguration(conf)
    .AddAudioBookConfiguration(conf)
    .AddLocalStorage()
    .AddAudioBook();
var serviceProvider = serviceCollection.BuildServiceProvider();

// Services
var bookRepo = serviceProvider.GetRequiredService<IBookRepository>();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
var bookCreator = serviceProvider.GetRequiredService<IAudioBookCreator>();

//BookInputAnalysis.SearchForSubstring("C:/git/haw-story/book", "&");


// Doing
var book = await bookRepo.LoadBookFromInput();
await serviceProvider.ExplorErrors();
//var createdChapters = await bookCreator.CreateBook(book,17);
//BookInputAnalysis.SearchForDoubleCharacters(book);



//await ManualMappingAnalysis.AnalyseMappings(serviceProvider);



//creator.ConcatenateAudioFiles(files, outputFile);


//await VoiceExplorations.AdjustSsmlGeneration(serviceProvider);

//await VoiceExplorations.ExploreJohnVoices(serviceProvider);
/*

var defaultVoiceForNow = SsmlConstants.Voices.Women.EnAUTina;
var narratorVoice = SsmlConstants.Voices.Men.EnUSAndrew;
var allSsmlEntries = book.Chapters
    .SelectMany(chap => chap.ToSsmlModel(
        narrator: narratorVoice,
        narratorConfig: null,
        defaultVoice: defaultVoiceForNow
        ).Select(entry => (
           Entry: entry, 
           AllCasedWords: chap.AllCharacters
              .Select(_ => _.CharacterName)
              .Where(_ => !_.Contains(" "))
              .Distinct().ToList()))
    )
    .Select((_,indx) => (SsmlEntry: _.Entry, Index: indx, _.AllCasedWords))
    .ToList();

var saveResult = await ssmlRepo.Save(allSsmlEntries, _ => _.SsmlEntry.ToSsmlStorageEntry(_.Index, narratorVoice, _.AllCasedWords));

logger.LogInformation("Save results: ");
foreach(var res in saveResult)
{
    logger.LogInformation($"  {res.FileName}: {res.Input.SsmlEntry.SemanticId}");
}

await Task.Delay(1_500);

var reloaded = await ssmlRepo.Load();
logger.LogInformation("Reload results: ");

var outputDir = "c:/temp2/ssmlexports";
if(Directory.Exists(outputDir))
    Directory.Delete(outputDir, true);
Directory.CreateDirectory(outputDir);

foreach (var res in reloaded.Where(_ => _.SemanticId.ToLower().StartsWith("20240410angelagood")))
{
    var outputFile = $"{outputDir}/{res.OrderIndex.ToString("000") + "_" +  res.SemanticId}.ssml";
    File.WriteAllText(outputFile, res.Entry);
}

await Task.Delay(3_000);

*/

//CharacterMappingGenerator.GenerateCharacterMappingFile(book);