using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
public record PlaybookChapter(
    string ChapterSemanticId,
    int ChapterIndex,
    string ChapterName,
    IReadOnlyCollection<PlaybookNode> Nodes,
    string EntryShaHash,
    string? SsmlContentHash = null,
    string? Mp3FileName = null
    )
{
}
