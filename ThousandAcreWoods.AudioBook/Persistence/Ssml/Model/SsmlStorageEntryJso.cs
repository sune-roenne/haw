using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using System.Formats.Tar;

namespace ThousandAcreWoods.AudioBook.Persistence.Ssml.Model;
internal record SsmlStorageEntryJso(
    string SemanticId,
    string Base64SsmlContents,
    string ShaHash,
    int OrderIndex
    )
{
    public StorageEntry<string> ToSsml(string subLocation) => new StorageEntry<string>(
        SemanticId: SemanticId,
        Entry: Encoding.UTF8.GetString(Convert.FromBase64String(Base64SsmlContents)),
        ShaHash: ShaHash,
        OrderIndex: OrderIndex,
        SubLocation: subLocation
        );
}

internal static class SsmlStorageEntryJsoExtensions
{
    public static SsmlStorageEntryJso ToJso(this StorageEntry<string> ent) => new SsmlStorageEntryJso(
        SemanticId: ent.SemanticId,
        Base64SsmlContents: Convert.ToBase64String(Encoding.UTF8.GetBytes(ent.Entry)),
        ShaHash: ent.ShaHash,
        OrderIndex: ent.OrderIndex
     );
}
