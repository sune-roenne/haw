using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Story.Model;
using ThousandAcreWoods.LocalStorage.Story.Model.ScriptPart;
using SP = ThousandAcreWoods.Domain.Story.Model.ScriptPart;


namespace ThousandAcreWoods.LocalStorage.Story.Model.Mappers;
internal static class SceneJsoMapper
{

    public static Scene ToDomain(this SceneJso scen, IReadOnlyDictionary<string, StoryCharacter> characters, IReadOnlyDictionary<string, StoryImage> images) => new Scene(
        SceneId: scen.SceneId,
        SceneName: scen.SceneName,
        SceneType: SceneType.Image,
        ScriptParts: scen.Script
            .Select(_ => _.ToDomain(characters))
            .ToList(),
        Image: scen.ImageName
           .PipeOpt(im => images.GetValueOrDefault(im))
        );


    public static SP ToDomain(this ScriptPartJso sp, IReadOnlyDictionary<string, StoryCharacter> characters)
    {
        var subtitles = sp switch
        {
            MultiSubtitleScriptPartJso mps => mps.Subtitles
               .Select(_ => _.ToDomain(characters))
               .ToList(),
            SingleSubtitleScriptPartJso sps => new List<Subtitle> { new Subtitle(
                Parts: ParseSubtitle(sps.Subtitle.Text, characters),
                Emphasize: false)
               },
            _ => []
        };

        var character = characters[sp.Character];
        return new SpeachSubtitleScriptPart(character, subtitles);
    }




    public static Subtitle ToDomain(this SubtitleJso sub, IReadOnlyDictionary<string, StoryCharacter> characters) => new Subtitle(
        Parts: ParseSubtitle(sub.Text, characters)
        );


    private static readonly Regex _characterReferencePattern = new Regex(@"\{\w+(:\d+)?\}");

    public static IReadOnlyCollection<SubtitlePart> ParseSubtitle(string text, IReadOnlyDictionary<string, StoryCharacter> characters)
    {
        var returnee = new List<SubtitlePart>();
        var characterReferences = _characterReferencePattern.Matches(text);
        if(characterReferences == null || !characterReferences.Any())
            returnee.Add(new PlainSubtitlePart(text));
        else
        {
            var previousIndex = 0;
            foreach (var m in characterReferences.ToList())
            {
                if (m.Index > previousIndex)
                {
                    var plainTextPrefix = text.Substring(previousIndex, m.Index - previousIndex);
                    returnee.Add(new PlainSubtitlePart(plainTextPrefix));
                }
                var matched = m.Value
                    .Replace(@"{", "")
                    .Replace(@"}", "");
                var (characterId, attributeName) = (matched, nameof(StoryCharacter.Name));
                if (matched.Split(":") is [string chara, string attribute])
                {
                    characterId = chara;
                    attributeName = attribute;
                }
                returnee.Add(new CharacterAttributeReferenceSubtitlePart(characters[characterId], attributeName));
                previousIndex = m.Index + m.Value.Length;
            }
            if (previousIndex + 1 < text.Length)
                returnee.Add(new PlainSubtitlePart(text.Substring(previousIndex, text.Length - previousIndex)));

        }
        return returnee;

    }


}
