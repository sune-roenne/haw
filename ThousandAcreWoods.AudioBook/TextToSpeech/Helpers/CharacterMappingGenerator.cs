using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.Persistence.Manual.Model;
using ThousandAcreWoods.AudioBook.Persistence.TextToSpeech.Model;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Helpers;
internal static class CharacterMappingGenerator
{

    public static void GenerateCharacterMappingFile(BookRelease book)
    {
        var outputFileName = AudioBookConfiguration.CharacterMappingFileName;
        if (File.Exists(outputFileName))
            return;
        var manualFolder = AudioBookConfiguration.ManualDataFolderAbsolutePath;
        if(!Directory.Exists(manualFolder))
            Directory.CreateDirectory(manualFolder);
        var allCharacters = book.Chapters
            .SelectMany(chap =>
               chap.ChapterContents
                  .Select(cont => (BookCharacter?)(cont switch
                  {
                      BookCharacterLine lin => lin.Character,
                      BookCharacterStoryTime st => st.Character,
                      BookSinging sin => sin.Character,
                      _ => null
                  })).Where(_ => _ != null)
                  .Select(_ => _!)
            ).ToList();
        var allCharacterKeys = allCharacters
            .Select(_ => _.CharacterFormatKey)
            .Distinct()
            .Order()
            .ToList();

        var toCreate = allCharacterKeys
            .Select(key => new ManualCharacterMappingJso(
                CharacterName: key,
                Voice: "",
                Prosody: new TextToSpeechProsodySpecificationJso(),
                Role: null,
                Style: null
                ))
            .ToList();
        var asJson = JsonSerializer.Serialize(toCreate, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(outputFileName, asJson);

    }

}
