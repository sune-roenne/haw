using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Layout;

public partial class SiteLayout
{
    [Inject]
    public IJSRuntime JsRuntime { get; set; }
    [Inject]
    public ILocalStorageRepository LocalStorageRepository { get; set; }

    [Inject]
    public IChapterLoader ChapterLoader { get; set; }

    private SiteScreenData? _siteScreenData;
    private ReadingPreferences? _readingPreferences;
    private SiteState? _siteState;
    protected override async Task OnParametersSetAsync()
    {
        var doUpdate = false;
        if(_readingPreferences == null)
        {
            _readingPreferences = new ReadingPreferences(OnUpdate);
            doUpdate = true;
        }
        if (_siteState == null)
        {
            _siteState = new SiteState(OnUpdate, LocalStorageRepository, ChapterLoader);
            await _siteState.Initialize();
            doUpdate = true;
        }
        if (doUpdate)
            await InvokeAsync(StateHasChanged);

    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(_siteScreenData == null)
        {
            var width = await JsRuntime.InvokeAsync<int>("transferScreenWidth");
            var height = await JsRuntime.InvokeAsync<int>("transferScreenHeight");
            var userAgent = await JsRuntime.InvokeAsync<string>("transferUserAgent");

            _siteScreenData = new SiteScreenData(Width:  width, Height: height, UserAgent: userAgent);
            await InvokeAsync(StateHasChanged);
        }
    }

    private void OnUpdate()
    {
        InvokeAsync(StateHasChanged);
    }

}
