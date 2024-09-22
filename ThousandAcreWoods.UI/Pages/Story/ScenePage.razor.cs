using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Application.Story;
using ThousandAcreWoods.Domain.Story.Model;

namespace ThousandAcreWoods.UI.Pages.Story;

public partial class ScenePage
{

    [Inject]
    public IStoryLoader StoryLoader { get; set; }


    private Scene? _scene;

    protected async override Task OnParametersSetAsync()
    {
        _scene ??= await StoryLoader.LoadScene("JOBINTV");
        await InvokeAsync(StateHasChanged);
    }



}
