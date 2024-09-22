using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NYK.Collections.Extensions;
using System.Text.Json;
using ThousandAcreWoods.Book.Hosting.Wasm.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public partial class ChapterComponent : IDisposable
{
    private const string VideoFolder = "videos";
    private const string ContentHolderId = "taw-site-chapter-text-holder";

    private static event EventHandler<DisplayScrollingStatus> OnScrollStatusChanged;

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public IChapterLoader ChapterLoader { get; set; }

    [CascadingParameter]
    public SiteScreenData ScreenData { get; set; }

    [CascadingParameter]
    public ReadingPreferences Preferences { get; set; }

    [CascadingParameter]
    public SiteState SiteState { get; set; }

    private SiteChapter? _chapter;
    private bool _showText = false;
    private Task? _textActivationTask;
    private bool _hasRegisteredEventHandler = false;
    private CancellationTokenSource _scrollStatusFollowUpCancellation = new CancellationTokenSource();
    private Task? _scrollStatusFollowUpper;

    [JSInvokable]
    public static Task UpdateScrollingStatus(int paragraphIndex, bool isAtBottom)
    {
        OnScrollStatusChanged?.Invoke(null, new DisplayScrollingStatus(paragraphIndex, isAtBottom));
        return Task.CompletedTask;
    }

    protected override async Task OnParametersSetAsync()
    {
        if (!_hasRegisteredEventHandler)
        {
            OnScrollStatusChanged += OnScrollChanged;
            _hasRegisteredEventHandler = true;
        }
        _chapter = await ChapterLoader.LoadChapter(SiteState.CurrentChapter);
        await InvokeAsync(StateHasChanged);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if(_textActivationTask == null)
        {
            _textActivationTask = Task.Run(async () =>
            {
                await Task.Delay(2_000);
                _showText = true;
                SiteState.SetFunctionalityDisablement(false);
                await InvokeAsync(StateHasChanged);
            });
        }
        if (_scrollStatusFollowUpper == null)
        {
            _scrollStatusFollowUpper = Task.Run(async () =>
            {
                while (!_scrollStatusFollowUpCancellation.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(5_000, _scrollStatusFollowUpCancellation.Token);
                        var scrollStatusString = await JSRuntime.InvokeAsync<string>("getScrollStatus");
                        var scrollStatus = JsonSerializer.Deserialize<DisplayScrollingStatus>(scrollStatusString);
                        if (scrollStatus != null) 
                           OnScrollChanged(null, scrollStatus!);
                    }
                    catch (Exception) { }
                }
            });
        }
        
    }

    private void ScrollTo(int paragraphIndex)
    {
        if(_chapter != null)
        {
            var paraId = DisplayChapterParser.ParagraghIdFor(_chapter.ChapterOrder, paragraphIndex);
            _ = Task.Run(async () =>
            {
                try
                {
                    await JSRuntime.InvokeVoidAsync("scrollToBlock", paraId);
                }
                catch (Exception e)
                {
                }
            });
        }
    }

    public void Dispose()
    {
        OnScrollStatusChanged -= OnScrollChanged;
        _scrollStatusFollowUpCancellation.Cancel();
    }

    private void OnScrollChanged(object? sender, DisplayScrollingStatus scrollingStatus)
    {
        if (_chapter != null && !SiteState.IsAutomatedReadingEnabled)
        {
            var status = scrollingStatus with { ChapterIndex = _chapter.ChapterOrder, ParagraphIndex = scrollingStatus.ParagraphIndex < 0 ? 0 : scrollingStatus.ParagraphIndex };
            SiteState.OnScrollingStatusChanged(status);
        }
    }


    private string? VideoFileName => _chapter?.PipeOpt(chap => $"{VideoFolder}/{chap.Video.FileName}");




}
