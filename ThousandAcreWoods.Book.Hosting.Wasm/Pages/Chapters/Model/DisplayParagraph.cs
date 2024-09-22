using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;

public record DisplayParagraph(
    IReadOnlyCollection<DisplayBlock> Blocks,
    FontSettings FontSettings,
    string ParagraphId,
    int ParagraphIndex,
    DisplayParagraphType ParagraphType
    );
