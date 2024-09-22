using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using ThousandAcreWoods.UI.Util;

namespace ThousandAcreWoods.UI.Components.TextUtilities;

public partial class FlashOnOffText
{
    private readonly long _id = UniqueId.NextId();

    private static readonly FontSettings DefaultFontSettings = new FontSettings(Family: "Blackadder ITC", Size: 30, Color: "rgba(255,255,255,1)");

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public FontSettings? FontSettings { get; set; }
    private FontSettings TheFontSettings => FontSettings + DefaultFontSettings;

    [Parameter]
    public TimeSpan InitialDelay { get; set; } = TimeSpan.FromSeconds(2);

    [Parameter]
    public bool LoopForever { get; set; }

    [Parameter]
    public TimeSpan TimeAlivePerCharacter { get; set; } = TimeSpan.FromSeconds(0.5);
    [Parameter]
    public TimeSpan TimeToAnimatePerLetter { get; set; } = TimeSpan.FromSeconds(0.2);
    [Parameter]
    public TimeSpan? CooldownPeriod { get; set; }
    [Parameter]
    public decimal OpacityOnShowing { get; set; } = 1m;



    private double _startAnimatingInPercentage;
    private double _endAnimatingInPercentage;
    private double _startAnimatingOutPercentage;
    private double _endAnimatingOutPercentage;
    private double _totalAnimationSeconds;



    protected override async Task OnParametersSetAsync()
    {
        CalculateAnimationData();
        await InvokeAsync(StateHasChanged);
    }


    private void CalculateAnimationData()
    {
        var finishedIntroTime = Text.Length * TimeToAnimatePerLetter.TotalSeconds;
        var totalAliveTime = Text.Length * TimeAlivePerCharacter.TotalSeconds;
        var totalLifeCycleTime = finishedIntroTime + totalAliveTime + TimeToAnimatePerLetter.TotalSeconds;
        if (CooldownPeriod != null)
            totalLifeCycleTime += CooldownPeriod.Value.TotalSeconds;
        else
            totalLifeCycleTime = totalLifeCycleTime * 2;
        if(totalLifeCycleTime != 0.0)
        {
            _totalAnimationSeconds = totalLifeCycleTime;
            var eachAnimationInPercent = TimeToAnimatePerLetter.TotalSeconds / totalLifeCycleTime;
            _startAnimatingInPercentage = 0;
            _endAnimatingInPercentage = eachAnimationInPercent;
            _startAnimatingOutPercentage = 0.5 - eachAnimationInPercent;
            _endAnimatingOutPercentage = 0.5;

        }

    }


    private MarkupString TextMarkup => Text.Select((cha, indx) =>
        $"<span class=\"taw-flash-on-off-text-{_id}-char-{indx}\">{cha}</span>"
        ).MakeString("").Pipe(_ => new MarkupString(_));

    private double DelayFor(int indx) => (indx * TimeToAnimatePerLetter.TotalSeconds + InitialDelay.TotalSeconds);


    private MarkupString CssRules => Enumerable.Range(0, Text.Length)
        .Select(indx => CssRuleFor(indx))
        .MakeString("\r\n")
        .Pipe(_ => new MarkupString(_));

    private string CssRuleFor(int indx) => 
        $".taw-flash-on-off-text-{_id}-char-{indx} {{ " +
        $"filter:blur(40px); " +
        
        $"animation: visibility {_totalAnimationSeconds.AsCssSeconds()} linear {(LoopForever ? "infinite" : "")}; " +
        $"animation-delay: {DelayFor(indx).AsCssSeconds()};  }} ";


}
