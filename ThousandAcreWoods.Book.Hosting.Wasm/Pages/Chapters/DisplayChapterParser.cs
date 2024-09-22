using ThousandAcreWoods.Book.Hosting.Wasm.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public static class DisplayChapterParser
{
    public static string ParagraghIdFor(int chapterIndex, int paragraphIndex) => $"haw-site-chapter-paragraph-{chapterIndex}-{paragraphIndex}";

    public static IReadOnlyCollection<DisplayParagraph> ToDisplayModel(this SiteChapter chapter, FontSettings defaultFont)
    {
        var returnee = new List<DisplayParagraph>();
        foreach(var (con,indx) in chapter.ChapterContents.Select((_,indx) => (_,indx)))
        {
            var paragraphId = ParagraghIdFor(chapter.ChapterOrder, indx);
            if (con is SiteChapterSection sec)
                returnee.Add(new DisplayParagraph(sec.ToDisplayModel(), defaultFont, ParagraphId: paragraphId, ParagraphIndex: indx, ParagraphType: DisplayParagraphType.Section));
            else if (con is SiteCharacterLine lin)
                returnee.Add(new DisplayParagraph(lin.ToDisplayModel(), defaultFont, ParagraphId: paragraphId, ParagraphIndex: indx, ParagraphType: DisplayParagraphType.Interaction));
            else if (con is SiteCharacterStoryTime st)
                returnee.Add(new DisplayParagraph(st.ToDisplayModel(), defaultFont, ParagraphId: paragraphId, ParagraphIndex: indx, ParagraphType: DisplayParagraphType.StoryTime));
            else if (con is SiteContextBreak ct)
                returnee.Add(new DisplayParagraph(Blocks: [], defaultFont, ParagraphId: paragraphId, ParagraphIndex: indx, ParagraphType: DisplayParagraphType.ContextBreak));
            else if (con is SiteNarration narr)
                returnee.Add(new DisplayParagraph(narr.ToDisplayModel(), defaultFont, ParagraphId: paragraphId, ParagraphIndex: indx, ParagraphType: DisplayParagraphType.Narration));
            else if (con is SiteNarrationList lis)
                returnee.Add(new DisplayParagraph(lis.ToDisplayModel(), defaultFont, ParagraphId: paragraphId, ParagraphIndex: indx, ParagraphType: DisplayParagraphType.Narration));
            else if (con is SiteSinging sin)
                returnee.Add(new DisplayParagraph(sin.ToDisplayModel(), defaultFont, ParagraphId: paragraphId, ParagraphIndex: indx, ParagraphType: DisplayParagraphType.Interaction));
        }
        return returnee;
    }

    public static IReadOnlyCollection<DisplayBlock> ToDisplayModel(this SiteCharacterLine line)
    {
        if (!line.LineParts.Any())
            return [];
        var characterFont = line.Character.FontSettings.Calibrate();
        var returnee = new List<DisplayBlock>();
        returnee.Add(new DisplayBlock(Left: line.Character.CharacterName, Center: line.LineParts.First().PartText, Right: line.LineParts.First().Description, FontCenter: characterFont));
        foreach (var part in line.LineParts.Skip(1))
            returnee.Add(new DisplayBlock(Center: part.PartText, Right: part.Description, FontCenter: characterFont));
        return returnee;

    }

    public static IReadOnlyCollection<DisplayBlock> ToDisplayModel(this SiteChapterSection section) => [
        new DisplayBlock(Header: section.Title)
        ];

    public static IReadOnlyCollection<DisplayBlock> ToDisplayModel(this SiteCharacterStoryTime stor)
    {
        var returnee = new List<DisplayBlock>();
        returnee.Add(new DisplayBlock(Header: stor.Title));
        foreach (var par in stor.Paragraphs)
        {
            if (par is SiteCharacterStoryTime.SiteCharacterStoryTimeTextParagraph tx)
                returnee.Add(new DisplayBlock(FullLine: tx.Text));
            else if (par is SiteCharacterStoryTime.SiteCharacterStoryTimeItemListParagraph lis)
            {
                foreach (var item in lis.Items)
                    returnee.Add(new DisplayBlock(CenterListItem: item));
            }

        }
        return returnee;
    }

    public static IReadOnlyCollection<DisplayBlock> ToDisplayModel(this SiteNarration narr) => [
        new DisplayBlock(FullLine: narr.NarrationContent)
        ];

    public static IReadOnlyCollection<DisplayBlock> ToDisplayModel(this SiteNarrationList lis) => lis.Items
        .Select(it => new DisplayBlock(ListItem: it))
        .ToList();


    public static IReadOnlyCollection<DisplayBlock> ToDisplayModel(this SiteSinging sin)
    {
        var returnee = new List<DisplayBlock>();
        if (!sin.LinesSong.Any())
            return returnee;
        var first = sin.LinesSong.First();
        var characterFont = sin.Character.FontSettings.Calibrate() with { IsItalic = true };
        returnee.Add(new DisplayBlock(Left: $"{sin.Character.CharacterName} [singing]", Center: $"{first} 🎵", FontCenter: characterFont));
        foreach (var lin in sin.LinesSong.Skip(1))
            returnee.Add(new DisplayBlock(Center: lin + (lin.Trim().Length > 2 ?  " 🎵" : ""), FontCenter: characterFont));
        return returnee;
    }



}
