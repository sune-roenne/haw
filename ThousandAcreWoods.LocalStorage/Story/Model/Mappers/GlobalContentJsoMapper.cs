using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Story.Model;

namespace ThousandAcreWoods.LocalStorage.Story.Model.Mappers;
internal static class GlobalContentJsoMapper
{

    public static StoryCharacter ToDomain(this CharacterJso character) => new(
        Id: character.Id,
        Name: character.Name,
        ColorLight: character.ColorLight,
        ColorDark: character.ColorDark
    );

    public static StoryImage ToDomain(this ImageJso im) => new(
        ImageId: im.Id,
        ImagePath: im.Path
        );


}
