using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.State;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public partial class BlockCenterLineComponent
{
    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public TimeSpan DelayPerItem { get; set; }

    [Parameter]
    public FontSettings Font { get; set; }

    [Parameter]
    public bool Animate { get; set; }

    protected override Task OnParametersSetAsync()
    {
        var tessa = Text;
        return base.OnParametersSetAsync();
    }

}
