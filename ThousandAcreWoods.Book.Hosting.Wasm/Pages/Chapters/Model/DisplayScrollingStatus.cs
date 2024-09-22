namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;

public record DisplayScrollingStatus(
    int ParagraphIndex,
    bool IsAtBottom,
    int? ChapterIndex = null
    );
