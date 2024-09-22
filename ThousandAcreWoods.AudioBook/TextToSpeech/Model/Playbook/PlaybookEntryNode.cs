using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
public record PlaybookEntryNode(
    string SemanticId,
    string EntryContentShaHash,
    int? PauseInMillis,
    StorageEntry<string>? StorageEntry = null,
    string? SsmlFileName = null,
    string? SsmlContentShaHash = null,
    string? Mp3FileName = null,
    string? Mp3ContentShaHash = null,
    int SsmlContentLength = int.MaxValue 
    ) : PlaybookNode(Mp3FileName)
{
    public override string EntryShaHash => EntryContentShaHash;

    public override string SsmlShaHash => SsmlContentShaHash!;

    public override string Mp3ShaHash => Mp3ContentShaHash!;

    public override int SsmlEntryCharacterCount => SsmlContentLength;

    public override string SemanticNodeId => SemanticId;
}
