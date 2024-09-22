using ThousandAcreWoods.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Book.Model;
public record BookCharacterStoryTime(
    BookCharacter Character,
    string Title,
    string Story
    ) : BookChapterContent
{

    public IReadOnlyCollection<BookCharacterStoryTimeParagraph> Paragrahs = Story
        .Split("\r\n\r\n")
        .Select(_ => _.Trim())
        .Select(Parse)
        .ToList();

    private static BookCharacterStoryTimeParagraph Parse(string paragraphText) =>
        paragraphText
        .Split("\r\n")
        .ToList()
        .Pipe<List<string>, BookCharacterStoryTimeParagraph>(lis => (lis.TrueForAll(_ => _.StartsWith("-")) && lis.Count > 1) ?
           new BookCharacterStoryTimeItemListParagraph(lis
             .Select(_ =>
                 _.Skip(1)
                 .MakeString("")
                 .Trim()
             ).ToList()
           ) :
           new BookCharacterStoryTimeTextParagraph(lis.MakeString("\r\n"))
        );

    public abstract record BookCharacterStoryTimeParagraph();

    public record BookCharacterStoryTimeItemListParagraph(
        IReadOnlyCollection<string> Items
        ) : BookCharacterStoryTimeParagraph;

    public record BookCharacterStoryTimeTextParagraph(
        string Text
        ) : BookCharacterStoryTimeParagraph;


}
