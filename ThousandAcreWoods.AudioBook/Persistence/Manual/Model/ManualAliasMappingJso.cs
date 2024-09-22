using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;

namespace ThousandAcreWoods.AudioBook.Persistence.Manual.Model;
public record ManualAliasMappingJso(
    string From,
    DateTime? ChapterDate,
    string? ChapterOrder,
    IReadOnlyCollection<string> To
    )
{
    public ManualAliasMapping ToModel() => new ManualAliasMapping(
        From: From,
        ChapterDate: ChapterDate,
        ChapterOrder: ChapterOrder,
        To: To.ToList()
    );
}

public static class ManualAliasMappingJsoExtensions
{
    public static ManualAliasMappingJso ToJso(this ManualAliasMapping mapping) => new ManualAliasMappingJso(
        From: mapping.From,
        ChapterDate: mapping.ChapterDate,
        ChapterOrder: mapping.ChapterOrder,
        To: mapping.To.ToList()
    );
}


