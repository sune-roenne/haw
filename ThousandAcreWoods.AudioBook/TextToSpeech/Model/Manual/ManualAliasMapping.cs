using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;
public record ManualAliasMapping(
    string From,
    DateTime? ChapterDate,
    string? ChapterOrder,
    IReadOnlyCollection<string> To
    )
{
    public readonly string FromKey = From.ToLower().Trim();
}
