using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;

namespace ThousandAcreWoods.AudioBook.Persistence.Playbook.Model;
internal record PlaybookChapterJso(
    string ChapterSemanticId,
    int ChapterIndex,
    string ChapterName,
    IReadOnlyCollection<PlaybookNodeJso> Nodes,
    string EntryShaHash,
    string? SsmlContentHash = null,
    string? Mp3FileName = null
    )
{

    public PlaybookChapter ToModel() => new PlaybookChapter(
        ChapterSemanticId: ChapterSemanticId,
        ChapterIndex: ChapterIndex,
        ChapterName: ChapterName,
        Nodes: Nodes.Select(_ => _.ToModel()).ToList(),
        EntryShaHash: EntryShaHash,
        SsmlContentHash: SsmlContentHash,
        Mp3FileName: Mp3FileName
    );


}


internal static class PlaybookChapterJsoExtensions
{
    public static PlaybookChapterJso ToJso(this PlaybookChapter chap) => new PlaybookChapterJso(
        ChapterSemanticId: chap.ChapterSemanticId,
        ChapterIndex: chap.ChapterIndex,
        ChapterName: chap.ChapterName,
        Nodes: chap.Nodes.Select(_ => _.ToJso()).ToList(),
        EntryShaHash: chap.EntryShaHash,
        SsmlContentHash: chap.SsmlContentHash,
        Mp3FileName: chap.Mp3FileName
     );
}
