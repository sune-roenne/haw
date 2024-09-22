using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.AboutTheAuthor;

public partial class AboutTheAuthorComponent
{
    [Inject]
    public IChapterLoader DataLoader { get; set; }

    [CascadingParameter]
    public SiteState SiteState { get; set; }


    private SiteAboutTheAuthor? _data;

    protected override async Task OnParametersSetAsync()
    {
        _data = await DataLoader.LoadAboutTheAuthor();
        await InvokeAsync(StateHasChanged);
    }

    private void OnBackToStoryClicked()
    {
        SiteState.CurrentPageSelection = SitePageSelection.Chapters;
    }

}
