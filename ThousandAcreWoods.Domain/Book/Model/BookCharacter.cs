using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookCharacter(
    string CharacterName,
    BookCharacterInfo? CharacterInfo,
    string? Alias
    )
{
    public string CharacterKey = CharacterName.Trim().ToLower();
    public string CharacterFormatKey => Alias?.Trim()?.ToLower() ?? CharacterKey;

    public string CharacterAudioKey => CharacterFormatKey;
}
