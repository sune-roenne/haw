using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Setup;

public partial class ExpandSidePanelButtonComponent
{
    [Parameter]
    public int Size { get; set; }

    [CascadingParameter]
    public SiteState SiteState { get; set; }


    private void OnClick()
    {
        SiteState.CurrentPageSelection = SitePageSelection.Setup;
    }




}
