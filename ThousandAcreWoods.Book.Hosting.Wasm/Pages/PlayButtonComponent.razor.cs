using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages;

public partial class PlayButtonComponent
{

    [Parameter]
    public int Size { get; set; }

    [CascadingParameter]
    public SiteState SiteState { get; set; }


    private void OnClick()
    {
        if (SiteState.IsAutomatedReadingEnabled)
            SiteState.AutomateReading(false);
        else SiteState.AutomateReading(true);
    }

    private bool ShowPlay => !SiteState.IsAutomatedReadingEnabled;
    private string ButtonColor => (ShowPlay, SiteState.IsFunctionalityDisabled) switch
    {
        (true, false) => "rgb(25,235,42)",
        (true, true) => "rgba(165,179,164,0.8)",
        (false, false) => "rgb(252,207,3)",
        _ => "rgba(252,207,3,0.5)"
    };

}
