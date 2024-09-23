using ThousandAcreWoods.Language.Extensions;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;

namespace ThousandAcreWoods.AudioBook.Persistence.Playbook.Model;
internal record PlaybookNodeJso(
    string NodeType,
    string? SemanticId = null,
    int? PauseInMillis = null,
    string? EntryContentShaHash = null,
    string? SsmlFileName = null,
    string? SsmlContentShaHash = null,
    string? Mp3FileName = null,
    string? Mp3ContentShaHash = null,
    IReadOnlyCollection<PlaybookNodeJso>? EntryNodes = null
    )
{

    public PlaybookNode ToModel() => Enum.Parse<PlaybookNodeTypeJso>(NodeType) switch
    {
        PlaybookNodeTypeJso.EntryNode => new PlaybookEntryNode(
            SemanticId: SemanticId!,
            EntryContentShaHash: EntryContentShaHash!,
            PauseInMillis: PauseInMillis,
            SsmlFileName: SsmlFileName,
            SsmlContentShaHash: SsmlContentShaHash,
            Mp3FileName: Mp3FileName,
            Mp3ContentShaHash: Mp3ContentShaHash
            ),
        PlaybookNodeTypeJso.MergedNode when EntryNodes != null => new PlaybookMergedNode(
            EntryNodes: EntryNodes
               .Select(_ => _.ToModel())
               .Collect(_ => _ as PlaybookEntryNode),
            Mp3FileName: Mp3FileName,
            Mp3ContentShaHash: Mp3ContentShaHash
            ),
        _ => throw new NotSupportedException()
    };


}


internal static class PlaybookNodeTypeJsoExtensions
{
    public static PlaybookNodeJso ToJso(this PlaybookNode node) => node switch
    {
        PlaybookEntryNode nod => new PlaybookNodeJso(
            NodeType: PlaybookNodeTypeJso.EntryNode.ToString(),
            SemanticId: nod.SemanticId,
            PauseInMillis: nod.PauseInMillis,
            EntryContentShaHash: nod.EntryContentShaHash,
            SsmlFileName: nod.SsmlFileName,
            SsmlContentShaHash: nod.SsmlContentShaHash,
            Mp3FileName: nod.Mp3FileName,
            Mp3ContentShaHash: nod.Mp3ContentShaHash
            ),
        PlaybookMergedNode merg => new PlaybookNodeJso(
            NodeType: PlaybookNodeTypeJso.MergedNode.ToString(),
            Mp3FileName: merg.Mp3FileName,
            Mp3ContentShaHash: merg.Mp3ContentShaHash,
            EntryNodes: merg.EntryNodes.Select(_ => _.ToJso()).ToList()
            ),
        _ => throw new NotSupportedException()
    };
}

