using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookCharacterInfo(
    string Character,
    string ShowName,
    string Color,
    string? Font,
    string? SiteFont
    )
{
    public string CharacterKey = Character.Trim().ToLower();


}
