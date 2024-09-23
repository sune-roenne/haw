using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThousandAcreWoods.Language.Extensions;
using ThousandAcreWoods.Application.Book.Persistence;
using ThousandAcreWoods.AudioBook.Persistence.Manual;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Helpers;
internal static class ManualMappingAnalysis
{
    public static async Task AnalyseMappings(IServiceProvider serviceProvider)
    {
        var manualMapRepo = serviceProvider.GetRequiredService<IManualMappingsRepository>();
        var charMaps = await manualMapRepo.LoadCharacterMappings();
        var aliasMaps = await manualMapRepo.LoadAliasMappings();
        var logger = serviceProvider.GetRequiredService<ILogger<IManualMappingsRepository>>();
        var bookLoader = serviceProvider.GetRequiredService<IBookRepository>();
        var book = await bookLoader.LoadBookFromInput();
        foreach(var chap in book.Chapters)
        {
            AnalyseCharacterMappings(charMaps, aliasMaps, chap, logger);
        }

    }

    private static void AnalyseCharacterMappings(
    IEnumerable<ManualCharacterMapping> characterMappings,
    IEnumerable<ManualAliasMapping> aliasMappings,
    BookChapter chapter,
    ILogger logger
    )
    {
        var aliasMap = aliasMappings
            .GroupAndToDictionary(_ => _.FromKey);
        var characterMap = characterMappings
            .ToDictionarySafe(_ => _.CharacterKey);
        foreach (var character in chapter.AllCharacters)
        {
            var characterSsmlId = character.CharacterAudioKey;
            if (aliasMap.TryGetValue(characterSsmlId, out var aliasMappas))
            {
                var relevantAliasMappings = aliasMappas
                    .Where(_ => (_.ChapterDate == null && _.ChapterOrder == null) ||
                         (_.ChapterDate == chapter.ChapterDateToUse && _.ChapterOrder == chapter.ChapterOrder)
                    )
                    .ToList();

                var mapping = aliasMappas
                    .OrderBy(_ => _.ChapterDate != null ? 1 : 2)
                    .FirstOrDefault();
                if (mapping != null)
                {
                    if (!mapping.To.Any())
                    {
                        logger.LogInformation($"Chapter: {chapter.ChapterDate.ToString("yyyy-MM-dd")}{chapter.ChapterOrder} - {chapter.ChapterName}: Alias FROM miss TO {mapping.From}");
                    }

                    var characterMappas = mapping.To
                        .Where(_ => !characterMap.ContainsKey(_))
                        .ToReadonlyCollection();
                    if (characterMappas.Any())
                    {
                        logger.LogInformation($"Chapter: {chapter.ChapterDate.ToString("yyyy-MM-dd")}{chapter.ChapterOrder} - {chapter.ChapterName}: Alias TO miss character {characterMappas.MakeString(",")}");
                    }
                    continue;
                }
            }
            if (!characterMap.ContainsKey(characterSsmlId))
            {
                logger.LogInformation($"Chapter: {chapter.ChapterDate.ToString("yyyy-MM-dd")}{chapter.ChapterOrder} - {chapter.ChapterName}: character not mapped {characterSsmlId}");
            }


        }

    }
    

}
