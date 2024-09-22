using ThousandAcreWoods.TextRendering.Pages.TextRendering.Model;
using System.Drawing.Text;
using ThousandAcreWoods.UI.Components.TextAnimation;
using ThousandAcreWoods.UI.Components.Common;
using NYK.Collections.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ThousandAcreWoods.TextRendering.Pages.TextRendering;

public partial class TextRenderingPage
{
    [Inject]
    public IJSRuntime Js { get; set; }


    private static readonly IReadOnlyCollection<AnimatedTextType> SelectableTypes = [
        AnimatedTextType.BlinkingNeonLight,
        AnimatedTextType.GlidingIn,
        AnimatedTextType.SlamIn,
        AnimatedTextType.FlashOnOff,
//        AnimatedTextType.BackgroundImage,
        AnimatedTextType.Glitch,
        AnimatedTextType.RotateIn
//        AnimatedTextType.Terminal
        ];

    private static readonly IReadOnlyCollection<AnimatedTextTimingFunction> TimingFunctions = [
        AnimatedTextTimingFunction.Linear,
        AnimatedTextTimingFunction.Ease,
        AnimatedTextTimingFunction.EaseIn,
        AnimatedTextTimingFunction.EaseOut,
        AnimatedTextTimingFunction.EaseInOut,
        AnimatedTextTimingFunction.Accelerating,
        AnimatedTextTimingFunction.AcceleratingBreakEnd,
        AnimatedTextTimingFunction.CubicBezier
        ];

    private static readonly IReadOnlyCollection<TextSplitMode> SplitModes = [
        TextSplitMode.ByWord,
        TextSplitMode.ByCharacter
        ];


    private static readonly IReadOnlyCollection<string> FontFamilies = new InstalledFontCollection().Families
        .Select(_ => _.Name)
        .Order()
        .ToList();


    private int? _fontSize = 100;
    private int? FontSize { get => _fontSize; set => Wrap(() => _fontSize = value); }

    private string? _color = "#2d05f5";//"#2d05f5";// Pink: "#ed34b3"; //Red: #a32929";
    private string? Color { get => _color; set => Wrap(() => _color = value); }

    private string? _fontFamily = "Trade Winds"; //"Protest Revolution"; //"Trade Winds";//"Shadows Into Light"; 
    private string? FontFamily { get => _fontFamily; set => Wrap(() => _fontFamily = value); }

    private AnimatedTextType? _textType = SelectableTypes.First();
    public AnimatedTextType? TextType { get => _textType; set => Wrap(() => _textType = value); }

    private AnimatedTextTimingFunction? _timingFunction = AnimatedTextTimingFunction.AcceleratingBreakEnd;
    private AnimatedTextTimingFunction? TimingFunction { get => _timingFunction; set => Wrap(() => _timingFunction = value); }

    private TextSplitMode _splitMode = SplitModes.First();
    private TextSplitMode SplitMode { get => _splitMode; set => Wrap(() => _splitMode = value); }

    private decimal _speed = 500;
    private decimal Speed { get => _speed; set => Wrap(() => _speed = value); }

    private string _text = "[biting her lip]";
    private string Text { get => _text; set => Wrap(() => _text = value); }

    private bool _record = false;
    private bool Record { get => _record; set => Wrap(() => _record = value); }


    private TimeSpan TimeToAnimate => TimeSpan.FromMilliseconds(50000/ ((int) _speed));


    private bool _enableAnimate = false;

    private string? _textToAnimate;
    private FontSettings? _fontSettings;

    private int[] FastBlinkinIndexes => new int[] { 1, 5, 8, 10, 13, 17 }
       .Pipe(_ => _textToAnimate == null ? [] : _.TakeWhile(_ => _ < _textToAnimate.Length).ToArray());
    private int[] SlowBlinkinIndexes => new int[] { 3, 6, 12, 13, 19, 22}
   .Pipe(_ => _textToAnimate == null ? [] : _.TakeWhile(_ => _ < _textToAnimate.Length).ToArray());



    protected override async Task OnParametersSetAsync()
    {
        CheckAnimateEnable();
        await Task.CompletedTask;
    }

    private void OnAnimateClick()
    {
        var toExecute = async () =>
        {
            _fontSettings = new FontSettings(
                Family: FontFamily,
                Size: FontSize,
                Color: Color.PipeOpt(_ => new HexColorSpecification(_))
                );
            _textToAnimate = Text;
            await InvokeAsync(StateHasChanged);
        };
        if (Record)
            StartCapture(toExecute);
        else
            _ = toExecute();
    }
    private void OnStopRecordingClick() => 
        _ = StopCapture();


    private void Wrap(Action action)
    {
        action();
        _textToAnimate = null;
        CheckAnimateEnable();
    }

    private void CheckAnimateEnable()
    {
        
        if (FontSize != null && Color != null && Color.Length > 2 && FontFamily != null && FontFamily.Length > 2 && Text != null && Text.Length > 1)
        {
            _enableAnimate = true;
            InvokeAsync(StateHasChanged);
        }
        else
        {
            _enableAnimate = false;
            InvokeAsync(StateHasChanged);
        }
    }

    private IJSObjectReference? _screenCapturer;
    private bool _startedRecording = false;


    private void StartCapture(Func<Task> toRecord)
    {
        _ = Task.Run(async () =>
        {
            _screenCapturer ??= await Js.InvokeAsync<IJSObjectReference>("import", "./javascript/screen-capture.js");
            if (!_startedRecording)
            {
                await _screenCapturer.InvokeVoidAsync("startRecording");
                _startedRecording = true;
                _ = Task.Run(async () =>
                {
                    await Task.Delay(2000);
                    await toRecord();
                });

            }
        });
    }
    private async Task StopCapture()
    {
        if (_screenCapturer != null)
        {
            await _screenCapturer.InvokeVoidAsync("stopRecording");
            _startedRecording = false;
        }
    }


}
