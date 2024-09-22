using Microsoft.AspNetCore.Components;
using NYK.Collections.Extensions;
using System.Text;
using ThousandAcreWoods.UI.Util;

namespace ThousandAcreWoods.UI.Components.TextUtilities;

public partial class SmoothFadeInText
{
    private readonly long _id = UniqueId.NextId();
    private readonly FontSettings FontDefaults = new FontSettings(Family: "Calibri", Size: 13, Color: "rgb(255, 255, 255)");

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public int CharactersPerLine { get; set; } = 50;

    [Parameter]
    public FontSettings? FontSettings { get; set; }
    private FontSettings TheFontSettings => FontSettings + FontDefaults;

    [Parameter]
    public int MillisDelayPerCharacter { get; set; } = 6;

    private string SmoothId => $"taw-text-smooth-in-{_id}";
    private string IdFor(string suffix) => $"{SmoothId}-{suffix}";

    private MarkupString TextElements => SplitText
        .Select(_ => _.IsNewLine ? "<br/>" : $"<span class=\"{IdFor("char")}  {IdFor("char") + "-" + _.Index}\" >{(_.Char == ' ' ? "&nbsp;" : _.Char)}</span>")
        .MakeString(" ")
        .Pipe(_ => new MarkupString(_));

    private string SpanRules => SplitText
        .Where(_ => !_.IsNewLine)
        .Select(_ => $".{IdFor("char") + "-" + _.Index} {{ animation: {IdFor("animation")} {MillisDelayPerCharacter * _.Index}ms ease-out 1 both; {(_.IsSpace ? "": "margin-left:-3px;")}  }}")
        .MakeString("\r\n");

    private string GeneralRules => $@"

    .{(IdFor("char"))} {{
        font-family: '{(TheFontSettings.TheFontFamily)}';
        color: {(TheFontSettings.TheFontColor)};
        font-size: {(TheFontSettings.TheFontSize)}px;
        height: {(TheFontSettings.TheFontSize)}px;
        display: inline-block;
    }}

    @keyframes {(IdFor("animation"))} {{
            0% {{
                opacity: 0;
                transform: perspective(500px) translate3d(-35px, -40px, -150px) rotate3d(1, -1, 0, 35deg);
            }}
            100% {{
                opacity: 1;
                transform: perspective(500px) translate3d(0, 0, 0);
            }}
    }}
";


    private IReadOnlyCollection<(char Char, int Index, bool IsSpace, bool IsNewLine)> SplitText => SplitTextToLines()
        .SelectMany(line => line.Select((ch, indx) => (Char: ch, IsSpace: ch == ' ', IsNewLine: false)).Append((Char: ' ', IsSpace: false, IsNewLine: true)))
        .Select((ent, indx) => (ent.Char, Index: indx, ent.IsSpace, ent.IsNewLine))
        .ToList();



    private IReadOnlyCollection<string> SplitTextToLines()
    {
        var returnee = new List<string>();
        var current = new List<string>();
        var remaining = new Queue<string>(Text.Split());
        while (remaining.TryDequeue(out var next))
        {
            if (current.Count > 0 && current.Sum(_ => _.Length + 1) + next.Length > CharactersPerLine)
            {
                returnee.Add(current.MakeString(" "));
                current = new List<string>();
            }
            current.Add(next);
        }
        if (current.Any())
            returnee.Add(current.MakeString(" "));
        return returnee;
    }



}
