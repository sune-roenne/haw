using Microsoft.Extensions.Options;
using ThousandAcreWoods.Language.Extensions;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.Persistence.Playbook;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Audio;
internal class AudioBookCreator : IAudioBookCreator
{
    private readonly AudioBookConfiguration _config;
    private readonly IPlaybookCreator _playbookCreator;
    private readonly IPlaybookRepository _playbookRepository;
    private readonly IAudioCreator _audioCreator;

    public AudioBookCreator(
        IOptions<AudioBookConfiguration> config, 
        IPlaybookCreator playbookCreator, 
        IPlaybookRepository playbookRepository, 
        IAudioCreator audioCreator)
    {
        _config = config.Value;
        _playbookCreator = playbookCreator;
        _playbookRepository = playbookRepository;
        _audioCreator = audioCreator;
    }

    public async Task<IReadOnlyCollection<PlaybookChapter>> CreateBook(BookRelease book, params int[] generateAudioForChapters)
    {
        var allChapters = book.Chapters
            .Select((chap, indx) => (Chapter: chap, Index: indx))
            .ToList();

        var relevantChapters = (generateAudioForChapters.Any() ?
              allChapters.Where(_ => generateAudioForChapters.Contains(_.Index)) :
              allChapters).ToList();

        var playbookChapters = await _playbookCreator.BuildPlaybookChapters(relevantChapters);
        var previouslySavedPlaybooks = await _playbookRepository.LoadChapters();
        playbookChapters = FillFromPrevious(playbookChapters, previouslySavedPlaybooks);

        var pairedChapters = playbookChapters
            .MatchedWith(relevantChapters, _ => _.ChapterIndex, _ => _.Index)
            .Select(_ => (PlaybookChapter: _.First, BookChapter: _.Second.Chapter))
            .ToList();
        var savedChapterTasks = pairedChapters
            .Select(async _ => (await _playbookRepository.SaveChapter(_.PlaybookChapter, _.BookChapter)).Pipe(sav => (PlaybookChapter: sav, _.BookChapter)))
            .ToList();
        await Task.WhenAll(savedChapterTasks);
        var savedChapters = savedChapterTasks
            .Select(_ => _.Result)
            .ToList();

        var generationTimeId = DateTime.Now.ToFileTime();
        var chaptersWithMp3s = new List<(PlaybookChapter PlaybookChapter, BookChapter BookChapter)>();
        foreach(var savd in savedChapters)
        {
            var withMp3s = await GenerateNodeMp3Data(savd.PlaybookChapter, generationTimeId);
            chaptersWithMp3s.Add((withMp3s, savd.BookChapter));
        }

        var mp3SaveTasks = chaptersWithMp3s
            .Select(async wMp3 => await _playbookRepository.SaveChapter(wMp3.PlaybookChapter, wMp3.BookChapter))
            .ToList();
        await Task.WhenAll(mp3SaveTasks);
        var mp3SaveResults = mp3SaveTasks
            .Select(_ => _.Result)
            .ToList();

        var concatTasks = mp3SaveResults
            .Select(async _ => await _audioCreator.ConcatenateAudioFiles(_, generationTimeId))
            .ToList();
        await Task.WhenAll(concatTasks);
        var returnee = concatTasks
            .Select(_ => _.Result)
            .ToList();
        return returnee;
    }


    private async Task<PlaybookChapter> GenerateNodeMp3Data(PlaybookChapter chapter, long timeId)
    {
        var returneeNodes = new List<PlaybookNode>();
        foreach(var node in chapter.Nodes)
        {
            if(node is PlaybookEntryNode entNode)
            {
                var generated = await _audioCreator.CreateAudioFor(entNode, chapter.ChapterSemanticId, timeId);
                returneeNodes.Add(generated);
            }
            else if(node is PlaybookMergedNode mergNode)
            {
                var generated = await _audioCreator.CreateAudioFor(mergNode, chapter.ChapterSemanticId, timeId);
                returneeNodes.Add(generated);
            }
        }
        chapter = chapter with
        {
            Nodes = returneeNodes
        };
        return chapter;
    }


    private IReadOnlyCollection<PlaybookChapter> FillFromPrevious(IEnumerable<PlaybookChapter> newGenerations, IReadOnlyCollection<PlaybookChapter> previousChapters)
    {
        var previousMap = previousChapters
           .ToDictionarySafe(_ => _.ChapterIndex, _ => _);
        var returnee = newGenerations
            .Select(newChap =>
            {
                if (!previousMap.TryGetValue(newChap.ChapterIndex, out var oldChap))
                    return newChap;
                var oldContentBySemanticId = oldChap.Nodes
                   .Collect(_ => _ as PlaybookEntryNode)
                   .Where(_ =>
                       _.SsmlContentShaHash != null &&
                       _.Mp3FileName != null && 
                       _.Mp3ContentShaHash != null
                       )
                   .ToDictionarySafe(_ => _.SemanticId, _ => (_.SsmlContentShaHash, _.Mp3FileName, _.Mp3ContentShaHash));
                newChap = newChap with
                {
                    Nodes = newChap.Nodes
                        .Select(newNode => (newNode, oldContentBySemanticId.TryGetValue(newNode.SemanticNodeId, out var oldVals)) switch
                        {
                            (PlaybookEntryNode entNode, true) when entNode.SsmlContentShaHash == oldVals.SsmlContentShaHash => 
                                 entNode with
                                    {
                                        Mp3FileName = oldVals.Mp3FileName,
                                        Mp3ContentShaHash = oldVals.Mp3ContentShaHash
                                    },
                            _ => newNode
                        }).ToList() 
                };
                return newChap;
            }).ToList();

        return returnee;

    }


}
