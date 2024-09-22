using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.UI.Components.TextAnimation;

namespace ThousandAcreWoods.UI.Components.Common;
internal static class CommonUtil
{
    public static readonly CultureInfo CssCulture = new CultureInfo("en-US");
    public static string SplitterFor(this TextSplitMode splitMode) => splitMode switch
    {
        TextSplitMode.ByWord => " ",
        TextSplitMode.ByLine => "\n",
        TextSplitMode.BySentence => ".",
        _ => ""
    };

    public static string JoinerFor(this TextSplitMode splitMode) => splitMode switch
    {
        TextSplitMode.ByWord => " ",
        TextSplitMode.ByLine => "\r\n",
        TextSplitMode.BySentence => ". ",
        _ => ""
    };

    public static IReadOnlyCollection<TextSplitResult> SplitText(this string text, TextSplitMode splitMode, Func<TextSplitResult, TextSplitResult>? delayCalculator = null) => (splitMode switch
    {
        TextSplitMode.ByCharacter => text.Select((part, indx) => new TextSplitResult(Part: part + "", Index: indx, TimeSpan.Zero)).ToList(),
        _ => text.Split(splitMode.SplitterFor())
                 .Select((part, indx) => new TextSplitResult(Part: part, Index: indx, TimeSpan.Zero))
                 .ToList(),
    }).Pipe(
        res => delayCalculator == null ?
             res :
             res.PassOver(TimeSpan.Zero, accumulator: (inp, state) => delayCalculator(inp).Pipe(
                 withIndividualDelay => (withIndividualDelay with { DelayPart = withIndividualDelay.DelayPart + state }).Pipe(
                     withDelay => (withDelay, withDelay.DelayPart)
                 )))
        );

    public static string ToCssClassRules(
        this string text,
        TextSplitMode splitMode,
        Func<TextSplitResult, string> classGenerator,
        Func<TextSplitResult, string> rulesGenerator,
        Func<TextSplitResult, TextSplitResult>? delayCalculator = null
    ) => text
            .SplitText(splitMode, delayCalculator)
            .Select(_ => $".{classGenerator(_)} {{ {rulesGenerator(_)} }}")
            .MakeString("\r\n");

    public record TextSplitResult(
        string Part,
        int Index,
        TimeSpan DelayPart
    );

    public static MarkupString ToCssClassRulesMarkup(this IEnumerable<(string Key, object? Value)> attributes, string ruleSelector, Func<object, string>? objectFormatter = null, bool removeNulls = true) => new MarkupString($@".{ruleSelector} {{
          {attributes.ToCssRules(objectFormatter, removeNulls)}
       }}");


    public static string ToCssRules(this IEnumerable<(string Key, object? Value)> attributes, Func<object, string>? objectFormatter = null, bool removeNulls = true) => attributes
        .Where(_ => _.Value != null || !removeNulls)
        .Select(p => $"{p.Key}: {p.Value.AsCssRuleValue(objectFormatter)};")
     .MakeString("\r\n");



    public static string AsCssRuleValue(this object? value, Func<object, string>? formatter = null) => value switch
    {
        null => "null",
        UnquotedString unq => unq.Text,
        ColorSpecification colSpec => colSpec.AsCssString,
        int i => i.ToString(),
        long l => l.ToString(),
        decimal d => d.ToString("F4", CssCulture),
        double d => d.ToString("F4", CssCulture),
        _ when formatter != null => formatter(value!),
        _ => $"'{value.ToString()}'"
    };


    public static string ToCssRules(this TextSplitResult res, Func<TextSplitResult, IEnumerable<(string Key, object? Value)>> attributeGenerator) =>
        attributeGenerator(res).ToCssRules();

    public static MarkupString ToCssClassMarkup(this FontSettings font, string cssClass) => new List<(string Key, object? Value)>
    {
        ("font-family", font.FontFamily),
        ("font-size", font.TheFontSize.Pipe(size => $"{size}pt").AsUnquoted()),
        ("color", font.FontColor),
        ("font-style", font.IsItalic ? "italic".AsUnquoted() : null)
    }.ToCssClassRulesMarkup(cssClass);


    public static UnquotedString AsUnquoted(this string value) => new UnquotedString(value);

    public record UnquotedString(string Text);


}
