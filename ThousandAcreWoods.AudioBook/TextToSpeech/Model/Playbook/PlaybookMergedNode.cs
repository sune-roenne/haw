using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
public record PlaybookMergedNode(
    IReadOnlyCollection<PlaybookEntryNode> EntryNodes,
    string? Mp3FileName = null,
    string? Mp3ContentShaHash = null
    ) : PlaybookNode(Mp3FileName)
{
    public override string EntryShaHash => EntryNodes.AggregateHash("Merged",_ => _.EntryShaHash);

    public override string SsmlShaHash => EntryNodes.AggregateHash("Merged", _ => _.SsmlShaHash);

    public override string Mp3ShaHash => Mp3ContentShaHash!;

    public string SemanticId => EntryNodes
        .OrderBy(_ => _.SemanticId)
        .First().SemanticId + "_merge";

    public override string SemanticNodeId => SemanticId;
}
