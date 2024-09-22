using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages;

public partial class Home
{

    [Inject]
    public IChapterLoader ChapterLoader { get; set; }

    [CascadingParameter]
    public SiteScreenData? ScreenData { get; set; }

    [CascadingParameter]
    public SiteState SiteState { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if(ScreenData != null)
        {
            var tess = ScreenData.Width;
        }
    }

    private int? LogoTop => ScreenData?.Width switch
    {
        null => null,
        <= 600 => 50,
        <= 1000 => 100,
        _ => 150
    };

    private int? LogoWidth => ScreenData?.Width switch
    {
        null => null,
        int wi => (int)(wi * 0.5d)
    };

    private int? LogoHeight => ScreenData?.Width switch
    {
        null => null,
        int wi => (int)(wi * 0.25d)
    };


}
