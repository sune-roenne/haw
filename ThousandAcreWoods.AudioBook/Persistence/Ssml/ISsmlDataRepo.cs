using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;

namespace ThousandAcreWoods.AudioBook.Persistence.Ssml;
public interface ISsmlDataRepo
{

    Task<IReadOnlyCollection<(T Input, StorageEntry<string> StorageEntry, string FileName)>> Save<T>(IEnumerable<T> inputs, Func<T, StorageEntry<string>> mapper);
    Task<IReadOnlyCollection<StorageEntry<string>>> Load();

    Task<StorageEntry<string>> Load(PlaybookEntryNode node);

}
