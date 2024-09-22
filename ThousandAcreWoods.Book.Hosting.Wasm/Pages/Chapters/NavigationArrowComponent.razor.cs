using Microsoft.AspNetCore.Components;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public partial class NavigationArrowComponent
{
    [Parameter]
    public bool IsLeft { get; set; }

    [Parameter]
    public Action OnClick { get; set; }

    [Parameter]
    public bool IsDisabled { get; set; }
}
