using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Util;
using ThousandAcreWoods.UI.Components.Common;
using ThousandAcreWoods.UI.Components.Util;

namespace ThousandAcreWoods.UI.Components.TextAnimation;

public partial class AnimatedTextComponent
{
    protected readonly long _id = UniqueId.NextId();
    protected const string PartName = "part";
    protected const string WordName = "word";


    [Parameter]
    public FontSettings? FontSettings { get; set; } = null;

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public bool LoopForever { get; set; } = false;

    [Parameter]
    public TimeSpan? TimeAlivePerPart { get; set; } = null;

    [Parameter]
    public TimeSpan TimeToAnimatePerPart { get; set; } = TimeSpan.FromSeconds(0.5);

    [Parameter]
    public TextSplitMode TextSplitMode { get; set; } = TextSplitMode.ByCharacter;

    [Parameter]
    public AnimatedTextTimingFunction? TimingFunction { get; set; }

    protected virtual AnimatedTextTimingFunction TimingFunctionToUse() => TimingFunction ??  AnimatedTextTimingFunction.Linear;


    private IReadOnlyCollection<AnimatedWord> _words = [];

    protected (decimal TotalAnimationTimeInMillis, decimal AlivePointPercent, decimal? StartDiePercent) Lifecycle = (1m, 0m, null);


    protected virtual string ComponentId => $"taw-text-{GetType().Name.ToLower()}-{_id}";
    protected virtual string WordId(int wordIndex) => IdFor($"{WordName}-{wordIndex}");
    protected virtual string PartId(int partIndex) => IdFor($"{PartName}-{partIndex}");
    protected virtual string IdFor(string suffix) => $"{ComponentId}-{suffix}";
    protected virtual string ContainerId => IdFor("container");
    protected virtual string DeadPartCssRules => "";
    protected virtual string AlivePartCssRules => "";
    protected virtual IReadOnlyCollection<AnimationBreak>? AnimationBreaks => null;

    protected virtual FontSettings DefaultFontSettings => new FontSettings(Family: "Times New Roman", Size: 30, Color: new RgbColorSpecification("rgba(0,0,0,1)"));

    protected FontSettings FontSettingsToUse;



    protected virtual MarkupString? ComponentAnimation()
    {
        var returneeHolder = new StringBuilder();
        returneeHolder.AppendLine($"@keyframes {ComponentId} {{");

        var breakpoints = AnimationBreaks;

        if(breakpoints != null)
        {
            foreach(var bp in breakpoints.OrderBy(_ => _.ProgressInPercent))
                returneeHolder.AppendLine($"  {bp.ProgressInPercent.AsCssNumber()}% {{ {bp.AttributeValues.ToCssRules()}}}");
        }
        else
        {
            returneeHolder.AppendLine($" 0% {{ {DeadPartCssRules} }}");
            returneeHolder.AppendLine($"  {Lifecycle.AlivePointPercent.AsCssNumber()}%{{ {AlivePartCssRules}}}");
            if (Lifecycle.StartDiePercent != null)
            {
                returneeHolder.AppendLine($"  {Lifecycle.StartDiePercent.Value.AsCssNumber()}%{{ {AlivePartCssRules}}}");
                returneeHolder.AppendLine($"  100% {{ {DeadPartCssRules} }}");
            }
        }
        returneeHolder.AppendLine("}");

        return new MarkupString(returneeHolder.ToString());
    }

    protected virtual MarkupString PartAnimationRules()
    {
        var returneeHolder = new StringBuilder();
        foreach(var part in _words.SelectMany(_ => _.Parts))
        {
            var currentDelay = Convert.ToDecimal(part.PartIndex * TimeToAnimatePerPart.TotalMilliseconds);
            returneeHolder.AppendLine($".{part.PartId} {{");
            returneeHolder.AppendLine($"  animation-name: {ComponentId};");
            returneeHolder.AppendLine($"  animation-duration: {Lifecycle.TotalAnimationTimeInMillis.AsCssNumber()}ms;");
            returneeHolder.AppendLine($"  animation-delay: {currentDelay.AsCssNumber()}ms;");
            returneeHolder.AppendLine($"  animation-timing-function: {TimingFunctionString(TimingFunctionToUse())}; ");

            if (LoopForever)
            {
                returneeHolder.AppendLine($"  animation-iteration-count: infinite;");
                returneeHolder.AppendLine($"  animation-direction: normal;");
                returneeHolder.AppendLine($"  animation-fill-mode: backwards;");
            }
            else
            {
                returneeHolder.AppendLine($"  animation-iteration-count: 1;");
                returneeHolder.AppendLine($"  animation-fill-mode: both;");
            }
            returneeHolder.AppendLine("}");
        }
        return new MarkupString(returneeHolder.ToString());
    }




    protected virtual MarkupString? FontCssRule() => FontSettingsToUse.ToCssClassMarkup(ContainerId);


    protected override Task OnParametersSetAsync()
    {
        FontSettingsToUse = FontSettings + DefaultFontSettings;
        if (!_words.Any())
            SplitText();
        CalculateLifecycle();
        return base.OnParametersSetAsync();
    }

    protected virtual void CalculateLifecycle()
    {
        var totalAnimationTimePerPart = TimeToAnimatePerPart + (TimeAlivePerPart?.Pipe(_ => _ + TimeToAnimatePerPart) ?? TimeSpan.Zero);
        var totalMillisPerPart = Convert.ToDecimal(totalAnimationTimePerPart.TotalMilliseconds);
        var alivePointPercent = 100m * Convert.ToDecimal(TimeToAnimatePerPart.TotalMilliseconds).SafeDivide(totalMillisPerPart);
        var startDiePointPercent = TimeAlivePerPart?.Pipe(_ => 100m - alivePointPercent);
        Lifecycle = (totalMillisPerPart, alivePointPercent ?? 100m, startDiePointPercent);
    }


    protected void SplitText()
    {
        _words = Text.SplitText(TextSplitMode.ByWord)
            .SelectMany(wrd => new List<CommonUtil.TextSplitResult> { wrd, new CommonUtil.TextSplitResult(" ", Index: 0, DelayPart: TimeSpan.Zero) })
            .Select((wrd, indx) => wrd with { Index = indx} )
            .Select(word => word.Part
                 .Pipe(prt => prt == " " ? new List<CommonUtil.TextSplitResult> { new CommonUtil.TextSplitResult(" ", 0, TimeSpan.Zero) } : prt.SplitText(TextSplitMode))
                 .Pipe(parts => (WordText: word.Part, WordIndex: word.Index, Parts: parts, PartsCount: parts.Count))
            )
            .PassThrough(0, (ent, prevCount) => (ent, prevCount + ent.PartsCount))
            .Select(p => (
                Parts: p.Input.Parts
                  .Select(_ => (Part: _, AbsolutePartIndex: p.Acummulated - p.Input.PartsCount + _.Index))
                  .Select(_ => new AnimatedPart(_.Part.Part, _.AbsolutePartIndex, PartId(_.AbsolutePartIndex)))
                  .ToList(),
                Entry: p.Input
               )
            ).Select(_ => new AnimatedWord(Text: _.Entry.WordText, WordIndex: _.Entry.WordIndex, WordId: WordId(_.Entry.WordIndex), Parts: _.Parts))
            .ToList();
           
    }

    protected virtual string TimingFunctionString(AnimatedTextTimingFunction timingFunction) => timingFunction switch
    {
        AnimatedTextTimingFunction.Ease => "ease",
        AnimatedTextTimingFunction.EaseIn => "ease-in",
        AnimatedTextTimingFunction.EaseOut => "ease-out",
        AnimatedTextTimingFunction.EaseInOut => "ease-in-out",
        AnimatedTextTimingFunction.Accelerating => "cubic-bezier(1,0,1,0)",
        AnimatedTextTimingFunction.AcceleratingBreakEnd => "cubic-bezier(1,0,0.6,1)",

        _ => "linear"
    };




    protected record AnimatedPart(string Text, int PartIndex, string PartId);

    protected record AnimatedWord(string Text, int WordIndex, string WordId, IReadOnlyCollection<AnimatedPart> Parts);




}
