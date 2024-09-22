using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;
using ThousandAcreWoods.UI.Components.TextUtilities;

namespace ThousandAcreWoods.UI.Pages.Captures;

public partial class CapturePage : IAsyncDisposable
{

    [Inject]
    public IJSRuntime JS { get; set; }

    private IJSObjectReference? _screenCapturer;
    private object? _captureStream;
    private bool _startedRecording = false;
    private AnimationTextType? _currentTextClass;
    private AnimationText? _currentText;
    private List<AnimationText>? _currentTexts;



    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _screenCapturer ??= await JS.InvokeAsync<IJSObjectReference>("import", "./javascript/screen-capture.js");
        if(!_startedRecording)
        {
            await _screenCapturer.InvokeVoidAsync("startRecording");
            _startedRecording = true;
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000);
                await RunAnimations();
            });

        }
    }

    private void OnAnimationComplete() => Task.Run(StopCapture);

    private async Task StopCapture()
    {
        if(_screenCapturer != null)
        {
            await _screenCapturer.InvokeVoidAsync("stopRecording");
            _startedRecording = false;
        }
    }

    private async Task RunAnimations()
    {
        foreach(var textTyp in _animationTypes)
        {
            _currentTextClass = textTyp;
            _currentTexts = new List<AnimationText>();
            foreach(var txt in textTyp.Texts)
            {
                _currentText = txt;
                _currentTexts.Add(txt);
                await InvokeAsync(StateHasChanged);
                await Task.Delay(txt.MillisDelay ?? 7000);

            }

        }
        await StopCapture();
    }



    public async ValueTask DisposeAsync()
    {
        if(_screenCapturer != null)
        {
            await _screenCapturer.DisposeAsync();
        }
    }

    private static readonly IReadOnlyCollection<AnimationTextType> _animationTypes = [
        new (nameof(FlashOnOffText), [new ("Jeg kaldte dig", MillisDelay: 5_000)], TextSize: 60),
        new (nameof(BlinkingNeonLights), [new ("PERFEKT", MillisDelay: 3_000, FontFamily: "Great Vibes", TextSize: 130)]),

        new (nameof(FlashOnOffText), [
            new ("Er der nogen der er friske på", MillisDelay: 5_000, Color: "rgb(41, 16, 232)", FontFamily: "Pristina"),
            new ("Verdens Værste Skattejagt?", MillisDelay: 5_000, Color: "rgb(41, 16, 232)", FontFamily: "Pristina")
            ], TextSize: 80
        ),

        new (nameof(FlashOnOffText), [
            new ("\"C ya bitches\"", MillisDelay: 5_000, Color: "rgb(41, 16, 232)", FontFamily: "Pristina")
            ], TextSize: 80
        ),
        new (nameof(FlashOnOffText), [
            new ("(det må du faktisk hellere specifikt lade være med)", MillisDelay: 8_000)], TextSize: 55
        ),

        new (nameof(FlashOnOffText), [
            new ("Jeg glæder mig til at se hvad du driver det til", MillisDelay: 8_000)], TextSize: 55
        ),
        new (nameof(BlinkingNeonLights), [new ("Jeg elsker dig", MillisDelay: 3_000, FontFamily: "Great Vibes", TextSize: 130)])
        ];



    private static readonly IReadOnlyCollection<AnimationTextType> _animationTypesFirst = [
        new (nameof(BlinkingNeonLights), [new ("Tillykke med fødselsdagen", MillisDelay: 3_000, FontFamily: "Great Vibes", TextSize: 130)]),
        new (nameof(FlashOnOffText), [new ("A mi querida hija", MillisDelay: 5_000)], TextSize: 60),
        new (nameof(FlashOnOffText), [new ("sin tí, no habría yo", MillisDelay: 5_000)], TextSize: 60),

        new (nameof(FlashOnOffText), [
            new ("De kaldte dig", MillisDelay: 5_000)
            ], TextSize: 40
        ),
        new (nameof(BlinkingNeonLights), [new ("Dukkebarn", MillisDelay: 5_000, FontFamily: "Great Vibes", TextSize: 100)]),

        new (nameof(FlashOnOffText), [
            new ("Jeg troede, at hvis jeg elskede dig nok", MillisDelay: 5_000),
            new ("ville du slippe for at arve min", MillisDelay: 5_000)
            ], TextSize: 60),

        new (nameof(GlitchText), [
            new ("TOMHED", MillisDelay: 5_000)
            ], TextSize: 60),

        new (nameof(FlashOnOffText), [
            new ("Der var ingen der havde fortalt mig hvor mange bekymringer der fulgte med at være far", MillisDelay: 7_000),
            new ("Det skete en del gange, at jeg afleverede dig i dagpleje eller børnehave", MillisDelay: 7_000),
            new ("cyklede et stykke væk hvor ingen kunne se mig", MillisDelay: 7_000),
            new ("og græd", MillisDelay: 5_000)
            ], TextSize: 60),

        new (nameof(FlashOnOffText), [
            new ("... and let me tell you ...", MillisDelay: 5_000),
            new ("vi snakker ikke en enkelt maskulin tåre der trillede ned af et stoneface ansigt...", MillisDelay: 7_000),
            new ("I UGLY CRIED!", MillisDelay: 5_000)
            ], TextSize: 60),

        new (nameof(FlashOnOffText), [
            new ("Jeg ønskede at du skulle lære at forsvare dig selv", MillisDelay: 5_000),
            new ("for jeg ønskede ikke at der skulle ske dig noget", MillisDelay: 5_000),
            new ("og jeg var bange for at jeg var for lille til at forsvare dig", MillisDelay: 7_000)
            ], TextSize: 60
        ),
        new (nameof(FlashOnOffText), [
            new ("men du var mere til svømning", MillisDelay: 5_000)
            ], TextSize: 60
        ),
        new (nameof(FlashOnOffText), [
            new ("Jeg forstod mig ikke så godt på svømning", MillisDelay: 3_000),
            new ("men første gang jeg så dig svømme til stævne", MillisDelay: 3_000),
            new ("(det var et julestævne i HI)", MillisDelay: 3_000),
            new ("og jeg hørte dine venner heppe \"SOFIJA\"", MillisDelay: 4_000),
            new ("fik jeg",  MillisDelay: 3_000)
            ], TextSize: 60
        ),

        new (nameof(BlinkingNeonLights), [new ("GÅSEHUD", MillisDelay: 5_000, TextSize: 180, Color: "rgb(149, 52, 235)", ShadowColor: "rgb(70, 22, 112)")]),


        new (nameof(FlashOnOffText), [
            new ("jeg var så STOLT", MillisDelay: 5_000),
            ], TextSize: 80
        ),
        new (nameof(FlashOnOffText), [
            new ("\"DET ER MIN DATTER DET DÉR!\"", MillisDelay: 5_000)
            ], TextSize: 100
        ),
        new (nameof(FlashOnOffText), [
            new ("Jeg har spillet rollen som fraværende far for længe til troværdigt at sige ting som:", MillisDelay: 8_000),
            new ("- \"Nu hvor du træder ind i de voksnes rækker, må du love mig...\"", MillisDelay: 6_000),
            new ("- \"I dit voksenliv skal du tage de ting jeg har lært dig med...\"", MillisDelay: 6_000),
            new ("- \"... bla bla bla... stolt videreføre traditioner... bla bla bla...\"", MillisDelay: 6_000),
            new ("- \"... bla bla bla... husk at besøge mig på plejehjemmet ... bla bla bla...\"", MillisDelay: 6_000)
            ], TextSize: 55
        ),
        new (nameof(FlashOnOffText), [
            new ("Men du skal vide at jeg ELSKER dig og er STOLT af dig!", MillisDelay: 8_000),
            new ("Jeg bliver glad af at tænke på den person du er", MillisDelay: 6_000),
            new ("- kvik", MillisDelay: 3_000),
            new ("- glad", MillisDelay: 3_000),
            new ("- sjov, med en humor der vist er lidt til den spydige side (den sjoveste side)", MillisDelay: 7_000),
            new ("- betænksom", MillisDelay: 3_000)
            ], TextSize: 60
        ),
        new (nameof(FlashOnOffText), [
            new ("De bedste og mest betydningsfulde dele af mit liv begyndte", MillisDelay: 8_000)
            ], TextSize: 80
        ),
        new (nameof(BlinkingNeonLights), [new ("tirsdag den 30. maj 2006", MillisDelay: 4_000, FontFamily: "Great Vibes", TextSize: 100)]),

        new (nameof(FlashOnOffText), [
            new ("De bedste og mest betydningsfulde dele af dit liv har du stadigvæk til gode", MillisDelay: 8_000)
            ], TextSize: 80
        ),


        new (nameof(FlashOnOffText), [
            new ("Destiny's calling, I'm like", MillisDelay: 5_000, Color: "rgb(41, 16, 232)", FontFamily: "Pristina")
            ], TextSize: 80
        ),
        new (nameof(FlashOnOffText), [
            new ("\"Baby come on in\"", MillisDelay: 5_000, Color: "rgb(41, 16, 232)", FontFamily: "Pristina")
            ], TextSize: 100
        )




        ];


    private record AnimationTextType(string Classname, IReadOnlyCollection<AnimationText> Texts, int TextSize = 40, string? FontFamily = null, string? Color = null);
    private record AnimationText(string Text, int? TextSize = null, int? MillisDelay = null, string? FontFamily = null, string? Color = null, string? ShadowColor = null);



}
