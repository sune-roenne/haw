using Microsoft.Extensions.Logging;
using NYK.Collections.Extensions;
using ThousandAcreWoods.AudioBook.Persistence.Manual;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
using ThousandAcreWoods.Domain.Book.Model;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Audio;
public class PlaybookCreator : IPlaybookCreator
{

    private readonly IManualMappingsRepository _mapRepo;
    private readonly ILogger<PlaybookCreator> _logger;


    public PlaybookCreator(IManualMappingsRepository mapRepo, ILogger<PlaybookCreator> logger)
    {
        _mapRepo = mapRepo;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<PlaybookChapter>> BuildPlaybookChapters(IEnumerable<(BookChapter Chapter, int Index)> bookChapters)
    {
        var characterMappings = await _mapRepo.LoadCharacterMappings();
        var (narrator, narratorConfig) = characterMappings.First(_ => _.CharacterKey == SsmlConstants.Voices.NarratorMappingName).Pipe(_ => (_.Voice, _.VoiceConfiguration));
        var aliasMappings = await _mapRepo.LoadAliasMappings();
        var chapters = bookChapters
            .Select(_ => BuildPlaybookChapter(_.Chapter, _.Index, characterMappings, aliasMappings, narrator, narratorConfig))
            .ToList();
        EnsureSemanticIdUniqueness(chapters);
        return chapters;
    }

    private PlaybookChapter BuildPlaybookChapter(
        BookChapter chapter, 
        int chapterIndex,
        IEnumerable<ManualCharacterMapping> characterMappingCollection,
        IEnumerable<ManualAliasMapping> aliasMappingsCollection,
        TextToSpeechVoiceDefinition narrator,
        TextToSpeechVoiceConfiguration? narratorConfig)
    {
        var chapterId = $"{chapter.ChapterDateToUse.ToString("yyyyMMdd")}{chapter.ChapterOrder}";
        var characterMap = BuildCharacterMappings(characterMappingCollection, aliasMappingsCollection, chapter);
        var casedWords = chapter.AllCharacters
            .Select(_ => _.CharacterName)
            .Where(_ => !_.Contains(" "))
            .ToHashSet();
        var headerContents = chapter.ToChapterTitle(chapterIndex, narrator, narratorConfig)
            .Pipe(titl => new PlaybookEntryNode(
                 SemanticId: $"Header {chapterId}",
                 PauseInMillis: null,
                 EntryContentShaHash: titl.Hash(),
                 StorageEntry: titl.ToSsmlStorageEntry(int.MaxValue, narrator, casedWords, chapterId),
                 SsmlContentLength: titl.SsmlContentCharacterCount
                 )
            ).Pipe(_ => _ with { SsmlContentShaHash = _.StorageEntry!.Entry.ToShaHash() });

        var contents = chapter.ChapterContents
            .Select((cont, sectionIndex) => (SectionIndex: sectionIndex +1, Content: cont))
            .ToList();

        var playbookNodes = contents
            .Select(cont => characterMap[cont.Content.CharacterAudioKey ?? SsmlConstants.Voices.NarratorMappingName] switch
            {
                IReadOnlyCollection<ManualCharacterMapping> lis when lis.Count > 1 => lis
                   .Select(charMap => cont.Content.ToSsmlEntry(chapter, narrator, narratorConfig, charMap.Voice, charMap.VoiceConfiguration, personDistinguisher: charMap.CharacterName))
                   .Pipe(entries => (PlaybookNode) new PlaybookMergedNode(
                                            EntryNodes: entries.Select(ent => 
                                                new PlaybookEntryNode(
                                                    SemanticId: $"{ent.SemanticId}_{ent.Voice.ShortName}",
                                                    PauseInMillis: ent.PauseInMillis,
                                                    StorageEntry: ent.ToSsmlStorageEntry(cont.SectionIndex, narrator, casedWords, chapterId),
                                                    EntryContentShaHash: ent.Hash(),
                                                    SsmlContentLength: ent.SsmlContentCharacterCount
                                                 ).Pipe(nod => nod with { SsmlContentShaHash = nod.StorageEntry!.Entry.ToShaHash()})
                                             ).ToList()
                   )),

             IReadOnlyCollection <ManualCharacterMapping> lis => lis.First()
                   .Pipe(charMap => cont.Content.ToSsmlEntry(chapter, narrator, narratorConfig, charMap.Voice, charMap.VoiceConfiguration, personDistinguisher: null))
                   .Pipe(ent => new PlaybookEntryNode(
                          SemanticId: ent.SemanticId,
                          PauseInMillis: ent.PauseInMillis,
                          StorageEntry: ent.ToSsmlStorageEntry(cont.SectionIndex, narrator, casedWords, chapterId),
                          EntryContentShaHash: ent.Hash(),
                          SsmlContentLength: ent.SsmlContentCharacterCount
                         ).Pipe(nod => nod with { SsmlContentShaHash = nod.StorageEntry!.Entry.ToShaHash() })
                   )
            }).ToList();

        playbookNodes = playbookNodes.Prepend(headerContents)
            .ToList();

        var chapterEntryHash = playbookNodes
            .AggregateHash($"{chapter.ChapterDateToUse.ToString("dd-MM-yyyy")}{chapterIndex}".ToShaHash(), _ => _.EntryShaHash);
        var playbookChapter = new PlaybookChapter(ChapterSemanticId: chapterId, chapterIndex, chapter.ChapterTitleToUse, playbookNodes, chapterEntryHash);
        return playbookChapter;
    }

    private static void EnsureSemanticIdUniqueness(IEnumerable<PlaybookChapter> chapters)
    {
        var groups = chapters
            .SelectMany(chap =>
               chap.Nodes.Where(_ => _.SsmlEntryCharacterCount > 20).SelectMany(_ => _ switch
               {
                   PlaybookEntryNode nod => [(nod.SemanticId, Node: nod)],
                   PlaybookMergedNode merg => merg.EntryNodes.Select(_ => (_.SemanticId, Node: _)),
                   _ => throw new Exception()
               }
               )
            ).GroupBy(_ => _)
            .Select(_ => (_.Key, Count: _.Count(), Nodes: _.Select(_ => _.Node).ToList()));
        var withMoreThanOne = groups
            .Where(_ => _.Count > 1)
            .ToList();
        if(withMoreThanOne.Any())
            throw new Exception($"Following semantic IDs had several entries: {withMoreThanOne.Select(_ => $"{_.Key}: {_.Count}").MakeString(", ")}");
            
    }


    private IReadOnlyDictionary<string, IReadOnlyCollection<ManualCharacterMapping>> BuildCharacterMappings(
        IEnumerable<ManualCharacterMapping> characterMappings,
        IEnumerable<ManualAliasMapping> aliasMappings,
        BookChapter chapter
        )
    {
        var aliasMap = aliasMappings
            .GroupAndToDictionary(_ => _.FromKey);
        var characterMap = characterMappings
            .ToDictionarySafe(_ => _.CharacterKey);
        var returneeHolder = new List<(string Key, IReadOnlyCollection<ManualCharacterMapping> CharacterMapping)>();
        var (narrMappingName, narratorMapping) = SsmlConstants.Voices.NarratorMappingName.Pipe(narMapNam => (Name: narMapNam, Mapping: characterMap[narMapNam]));
        returneeHolder.Add((narrMappingName, [narratorMapping]));
        foreach (var character in chapter.AllCharacters)
        {
            var characterSsmlId = character.CharacterAudioKey;
            try
            {
                if (aliasMap.TryGetValue(characterSsmlId, out var aliasMappas))
                {
                    var relevantAliasMappings = aliasMappas
                        .Where(_ => (_.ChapterDate == null && _.ChapterOrder == null) ||
                             (_.ChapterDate == chapter.ChapterDateToUse && _.ChapterOrder == chapter.ChapterOrder)
                        )
                        .ToList();

                    var mapping = aliasMappas
                        .OrderBy(_ => _.ChapterDate != null ? 1 : 2)
                        .FirstOrDefault();
                    if (mapping != null)
                    {
                        var characterMappas = mapping.To
                            .Select(_ => characterMap[_])
                            .ToReadonlyCollection();
                        returneeHolder.Add((characterSsmlId, characterMappas));
                        continue;
                    }
                }
                var mappa = characterMap[characterSsmlId];
                returneeHolder.Add((characterSsmlId, [mappa]));


            }
            catch (Exception)
            {
                _logger.LogError($"Failed to generate mapping for character: {character.CharacterName} in chapter: {chapter.ChapterName}");
                throw;
            }

        }
        var returnee = returneeHolder
            .ToDictionarySafe(_ => _.Key, _ => _.CharacterMapping);
        return returnee;
    }





}
