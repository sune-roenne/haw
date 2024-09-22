using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model;
public record  StorageEntry<TEntry>(
    string SemanticId,
    TEntry Entry,
    string ShaHash,
    int OrderIndex,
    string SubLocation
    ) where TEntry : class
{

}
