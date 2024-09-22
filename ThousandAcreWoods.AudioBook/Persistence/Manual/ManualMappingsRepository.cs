using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.Persistence.Manual.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;

namespace ThousandAcreWoods.AudioBook.Persistence.Manual;
internal class ManualMappingsRepository : IManualMappingsRepository
{
    public async Task<IReadOnlyCollection<ManualAliasMapping>> LoadAliasMappings()
    {
        var fileName = AudioBookConfiguration.AliasMappingFileName;
        var loaded = await File.ReadAllTextAsync(fileName);
        var parsed = JsonSerializer.Deserialize<IReadOnlyCollection<ManualAliasMappingJso>>(loaded)!;
        var mapped = parsed
            .Select(_ => _.ToModel())
            .ToList();
        return mapped;
    }

    public async Task<IReadOnlyCollection<ManualCharacterMapping>> LoadCharacterMappings()
    {
        var fileName = AudioBookConfiguration.CharacterMappingFileName;
        var loaded = await File.ReadAllTextAsync(fileName);
        var parsed = JsonSerializer.Deserialize<IReadOnlyCollection<ManualCharacterMappingJso>>(loaded)!;
        var mapped = parsed
            .Select(_ => _.ToModel())
            .ToList();
        return mapped;
    }
}
