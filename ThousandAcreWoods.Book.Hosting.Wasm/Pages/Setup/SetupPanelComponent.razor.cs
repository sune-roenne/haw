using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.Configuration;
using ThousandAcreWoods.Book.Hosting.Wasm.State;
using SiteStateClass = ThousandAcreWoods.Book.Hosting.Wasm.State.SiteState;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Setup;

public partial class SetupPanelComponent
{
    [Parameter]
    public bool IsCondensed { get; set; }

    [Parameter]
    public bool ShowBackToStoryButton { get; set; }


    [CascadingParameter]
    public SiteStateClass SiteState { get; set; }

    private void OnPlaySpeedChanged(ChangeEventArgs ev)
    {
        try
        {
            var newValue = decimal.Parse(ev.Value!.ToString()!) / 100m;
            SiteState.SetReadingSpeed(newValue);
        }
        catch { }
    }

    private void OnBackToStoryClicked()
    {
        SiteState.CurrentPageSelection = SitePageSelection.Chapters;
    }
    private string? DisableReadingSpeedRange => (SiteState.IsAutomatedReadingEnabled || SiteState.IsFunctionalityDisabled) ? "disabled" : null;
    private int MinRangeValue = (int) (SiteRenderingConstants.MinReadingSpeed * 100m);
    private int MaxRangeValue = (int)(SiteRenderingConstants.MaxReadingSpeed * 100m);

    private void OnAboutClicked()
    {
        SiteState.CurrentPageSelection = SitePageSelection.AboutTheAuthor;
    }


}
