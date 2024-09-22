using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.State;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public partial class ParagraphComponent : IDisposable
{

    private static readonly TimeSpan DefaultTimePerCharacter = TimeSpan.FromMilliseconds(100);
    private static readonly TimeSpan SmallDelay = TimeSpan.FromMilliseconds(200);
    private static readonly TimeSpan MediumDelay = TimeSpan.FromMilliseconds(800);

    [CascadingParameter]
    public SiteState SiteState { get; set; }

    [CascadingParameter]
    public ReadingPreferences Preferences { get; set; }
    public FontSettings DefaultFontSettings => Preferences.PreferredFont;

    [CascadingParameter]
    public SiteScreenData ScreenData { get; set; }

    [Parameter]
    public DisplayParagraph Paragraph { get; set; }

    [Parameter]
    public Action? OnFinishedRendering { get; set; } = null;

    [Parameter]
    public bool Animate { get; set; }

    private Queue<DisplayBlock> _blockQueue = new Queue<DisplayBlock>();

    private List<RenderingItem> _currentlyRendering = new List<RenderingItem>();
    private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
    private Task? _renderingTask;
    private bool _hasRegisteredForAutomaticReadingChangeEvent = false;

    private IReadOnlyCollection<RenderingItem> ForRender => _currentlyRendering.ToArray();

    private decimal SpeedMultiplier => 1m - (SiteState.CurrentReadingSpeed * 0.9m);

    private int MarginY => Paragraph.ParagraphType switch {
        DisplayParagraphType.ContextBreak => 20,
        DisplayParagraphType.Narration => 10,
        _ => 5
        };

    private void OnAutomaticReadingChange(object? sender, bool newValue)
    {
        if(newValue == false)
        {
            _cancellationToken.Cancel();
        }
    }

    protected override Task OnParametersSetAsync()
    {
        if(!_hasRegisteredForAutomaticReadingChangeEvent)
        {
            SiteState.OnAutomaticReadingChange += OnAutomaticReadingChange;
        }

        if (Animate)
        {
            if (!_blockQueue.Any() && Paragraph.Blocks.Any())
            {
                foreach (var block in Paragraph.Blocks)
                    _blockQueue.Enqueue(block);
            }
            if (_renderingTask == null)
            {
                _blockQueue.Clear();
                _cancellationToken = new CancellationTokenSource();
                _renderingTask = StartRendering();
            }
        }
        else 
        {
            _currentlyRendering.Clear();
            foreach (var block in Paragraph.Blocks)
                _currentlyRendering.Add(
                    new RenderingItem(
                        Block: block,
                        DelayPerCharacter: TimeSpan.Zero,
                        FontSettingsCenter: block.FontCenter + Paragraph.FontSettings,
                        TotalTime: TimeSpan.Zero)
                    { ShowRightSide = true }) ;
        }
        return Task.CompletedTask;
    }


    private Task StartRendering() => Task.Run(async () =>
    {
        try
        {
            var cancToken = _cancellationToken.Token;
            _currentlyRendering.Clear();
            while (!cancToken.IsCancellationRequested && _blockQueue.TryDequeue(out var block))
            {
                var speedMultiplier = Convert.ToDouble(SpeedMultiplier);
                var delayPerCharacter = DefaultTimePerCharacter * speedMultiplier;
                var totalRenderTime = CalculateTotalTime(block, delayPerCharacter);
                var renderItem = new RenderingItem(
                    Block: block,
                    DelayPerCharacter: delayPerCharacter,
                    FontSettingsCenter: (block.FontCenter + Paragraph.FontSettings).Pipe(_ => _ with { Size = _.Size + 2 }),
                    TotalTime: totalRenderTime);
                if (block.Header != null)
                    renderItem.ShowRightSide = true;
                _currentlyRendering.Add(renderItem);
                _ = InvokeAsync(StateHasChanged);
                await Task.Delay(totalRenderTime, cancToken);
                renderItem.ShowRightSide = true;
                await Task.Delay(MediumDelay, cancToken);
                renderItem.HasRendered = true;
            }
            await Task.Delay(SmallDelay * 5, cancToken);
            OnFinishedRendering?.Invoke();
            _currentlyRendering.Clear();
        }
        catch (Exception) { }
        _renderingTask = null;
    }, _cancellationToken.Token);

    private record RenderingItem(
        DisplayBlock Block,
        TimeSpan DelayPerCharacter,
        FontSettings FontSettingsCenter,
        TimeSpan TotalTime
        )
    {
        public bool HasRendered { get; set; }
        public bool ShowRightSide { get; set; } = false;
    }


    private TimeSpan CalculateTotalTime(DisplayBlock block, TimeSpan delayPerCharacter)
    {
        var stringToAnimate = block.FullLine ?? block.CenterListItem ?? block.ListItem ?? block.Center ?? " ";
        var numberOfPartsToRender = stringToAnimate.Split(" ").Length;
        var totalTime = numberOfPartsToRender * delayPerCharacter;
        return totalTime;
    }

    public void Dispose()
    {
        SiteState.OnAutomaticReadingChange -= OnAutomaticReadingChange;
    }
}
