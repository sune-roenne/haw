using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;

public record DisplayBlock(
    string? Left = null,
    string? Center = null,
    string? Right = null,
    string? FullLine = null,
    string? Header = null,
    string? ListItem = null,
    string? CenterListItem = null,
    FontSettings? FontCenter = null
    )
{
    public bool IsDoneRendering { get; set; }
}
