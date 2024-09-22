using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;

namespace ThousandAcreWoods.AudioBook.Persistence.Manual;
public interface IManualMappingsRepository
{
    Task<IReadOnlyCollection<ManualCharacterMapping>> LoadCharacterMappings();
    Task<IReadOnlyCollection<ManualAliasMapping>> LoadAliasMappings();

}
