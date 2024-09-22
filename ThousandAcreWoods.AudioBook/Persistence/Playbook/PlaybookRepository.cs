using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.Persistence.Playbook.Model;
using ThousandAcreWoods.AudioBook.Persistence.Ssml;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.Domain.Book.Model;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.AudioBook.Persistence.Playbook;
internal class PlaybookRepository : IPlaybookRepository
{
    private readonly string PlaybookDataFolder = $"{AudioBookConfiguration.ManagedDataFolderAbsolutePath}/playbook";

    private const string PlaybookFileEnding = "plb.json";

    private readonly ISsmlDataRepo _ssmlRepo;

    public PlaybookRepository(ISsmlDataRepo ssmlRepo)
    {
        _ssmlRepo = ssmlRepo;
    }

    public async Task<PlaybookChapter> SaveChapter(PlaybookChapter chapter, BookChapter bookChapter)
    {
        var allCasedWords = bookChapter.AllCharacters
            .Select(_ => _.CharacterName)
            .Where(_ => !_.Contains(" "))
            .ToHashSet();

        var toSave = chapter.Nodes
            .Collect(_ => _ as PlaybookEntryNode)
            .Concat(
               chapter.Nodes
                   .Collect(_ => _ as PlaybookMergedNode)
                   .SelectMany(_ => _.EntryNodes)
            ).ToList();

        var saved = await _ssmlRepo.Save(toSave, _ => _.StorageEntry!);
        var savedInfoMap = saved
            .ToDictionary(_ => _.Input.UniqueNodeId, _ => _.FileName);

        var returnee = chapter with
        {
            Nodes = chapter.Nodes
               .Select(node => node switch
               {
                   PlaybookEntryNode ent => ent with
                   {
                       SsmlFileName = savedInfoMap.GetValueOrDefault(ent.UniqueNodeId)
                   },
                   PlaybookMergedNode merg => merg with
                   {
                       EntryNodes = merg.EntryNodes.Select(
                           entNode => entNode with
                               {
                                    SsmlFileName = savedInfoMap.GetValueOrDefault(entNode.UniqueNodeId)
                               }
                           ).ToList()
                   },
                   _ => node
               }).ToList(),
            SsmlContentHash = chapter.Nodes
               .AggregateHash($"{chapter.ChapterIndex}", _ => _.SsmlShaHash)
        };
        await Save(returnee);

        return returnee;
    }

    private async Task Save(PlaybookChapter chapter)
    {
        if(!Directory.Exists(PlaybookDataFolder))
            Directory.CreateDirectory(PlaybookDataFolder);
        var converted = chapter.ToJso();
        var outputFileName = $"{PlaybookDataFolder}/{chapter.ChapterIndex.ToString("00")}_{chapter.ChapterName}.{PlaybookFileEnding}";
        if (File.Exists(outputFileName))
            File.Delete(outputFileName);
        var asString = JsonSerializer.Serialize(converted, options: new JsonSerializerOptions { WriteIndented = true});
        await File.WriteAllTextAsync(outputFileName, asString);
        //await File.Wr
    }

    public async Task<IReadOnlyCollection<PlaybookChapter>> LoadChapters()
    {
        var relevantFiles = Directory.GetFiles(PlaybookDataFolder)
            .Where(_ => _.ToLower().EndsWith(PlaybookFileEnding.ToLower()))
            .ToList();
        var loadTasks = relevantFiles
            .Select(async fil => (await File.ReadAllTextAsync(fil))
               .Pipe(_ => JsonSerializer.Deserialize<PlaybookChapterJso>(_))
               .Pipe(_ => _.ToModel())
            ).ToList();
        await Task.WhenAll(loadTasks);
        var returnee = loadTasks
            .Select(_ => _.Result)
            .ToList();
        return returnee;
    }
}
