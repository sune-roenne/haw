using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Language.Extensions;
using ThousandAcreWoods.UI.Util;

namespace ThousandAcreWoods.UI.Components.TextUtilities;

public partial class GlidingInText
{
    private readonly long _id = UniqueId.NextId();

    private static readonly FontSettings FontDefaults = new FontSettings(Family: "Algerian", Size: 150, Color: "rgba(255,255,255,1)");

    [Parameter]
    public string Text { get; set; }

    [Parameter]
    public FontSettings? FontSettings { get; set; }
    private FontSettings TheFontSettings => FontSettings + FontDefaults;

    [Parameter]
    public TimeSpan AnimationDelayPerCharacter { get; set; } = TimeSpan.FromSeconds(0.05);

    private string GlidingId => $"taw-text-gliding-in-{_id}";

    private string IdFor(string suffix) => $"{GlidingId}-{suffix}";




    private MarkupString SpannedText => Text
        .Select((ch,indx) => (Char: ch, Index: indx))
        .Select(_ => _.Char == ' ' ? " " : $"<span class=\"{IdFor("animate-span")}   {IdFor("span-" + _.Index)}\">{_.Char}</span>")
        .MakeString("")
        .Pipe(_ => new MarkupString(_));

    private string SpanRules => Text
        .Select((ch, indx) => (Char: ch, Index: indx))
        .Select(_ => $".{IdFor("span-" + _.Index)} {{ animation-delay: {(AnimationDelayPerCharacter.TotalSeconds * _.Index).AsCssNumber()}s; " +
        $"display: inline-block; }}")
        .MakeString("\r\n");

    private string GeneralAnimateSpanRule => $@"
     .{IdFor("animate-span")} {{
            opacity: 0;
            transform: translate(-150px, -50px) rotate(-180deg) scale(3);
            animation: {IdFor("revolveScale")} .4s forwards;
          
        }}

        @keyframes {IdFor("revolveScale")} {{
          60% {{
            transform: translate(20px, 20px) rotate(30deg) scale(.3);
          }}

          100% {{
            transform: translate(0) rotate(0) scale(1);
            opacity: 1;
          }}
        }}";

    private string ContainerRule => $@"
     .{IdFor("container")} {{
            font-family: '{TheFontSettings.TheFontFamily}';
            font-size: {TheFontSettings.TheFontSize}px;
            color: {TheFontSettings.TheFontColor};
      }}";

}
