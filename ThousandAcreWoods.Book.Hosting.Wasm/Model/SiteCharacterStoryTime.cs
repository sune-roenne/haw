using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ThousandAcreWoods.Book.Hosting.Wasm.Model.SiteCharacterStoryTime;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Model;
public record SiteCharacterStoryTime(
    SiteCharacter Character,
    string Title,
    IReadOnlyCollection<SiteCharacterStoryTimeParagraph> Paragraphs
    ) : SiteChapterContent
{


    public abstract record SiteCharacterStoryTimeParagraph();

    public record SiteCharacterStoryTimeItemListParagraph(
        IReadOnlyCollection<string> Items
        ) : SiteCharacterStoryTimeParagraph;

    public record SiteCharacterStoryTimeTextParagraph(
        string Text
        ) : SiteCharacterStoryTimeParagraph;


}
