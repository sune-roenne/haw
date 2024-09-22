using Microsoft.AspNetCore.Components;
using System.Text;
using ThousandAcreWoods.UI.Util;

namespace ThousandAcreWoods.UI.Components.TextUtilities;

public partial class TerminalText
{
    private readonly long _id = UniqueId.NextId();


    [Parameter]
    public IReadOnlyCollection<string> Text { get; set; }

    [Parameter]
    public TimeSpan DelayBetweenChars { get; set; } = TimeSpan.FromMilliseconds(80);

    [Parameter]
    public TimeSpan DelayBetweenLines { get; set; } = TimeSpan.FromMilliseconds(800);

    [Parameter]
    public Action? OnAnimationComplete { get; set; }

    private string _currentText = string.Empty;

    private MarkupString CurrentMarkup => new MarkupString(_currentText);

    private Task? _animationTask;


    protected override async Task OnParametersSetAsync()
    {
        if (_animationTask == null)
            _animationTask = StartAnimation();

        await Task.CompletedTask;
    }

    private string TerminalId => $"taw-text-terminal-{_id}";

    private string IdFor(string suffix) => $"{TerminalId}-{suffix}";


    private Task StartAnimation() => Task.Run(async () =>
    {
        var stringState = new StringBuilder();
        foreach(var line in Text)
        {
            foreach(var ch in line)
            {
                if (ch == ' ')
                    stringState.Append("&nbsp;");
                else 
                    stringState.Append(ch);
                _currentText = stringState.ToString();
                await InvokeAsync(StateHasChanged);
                await Task.Delay(DelayBetweenChars);
            }
            stringState.Append("<br/>");
            _currentText = stringState.ToString();
            await InvokeAsync(StateHasChanged);
            await Task.Delay(DelayBetweenChars);
        }
        _animationTask = null;
        OnAnimationComplete?.Invoke();
    });

    private string TextContainerRule => $@"
        .{IdFor("text-container")} {{
           position: absolute;
           bottom: 0;
           width: 100%;
           min-height: 100%;
           padding: 40px;
           font-size: 20px;
           line-heght: 25px;
           box-sizing: border-box;
           text-align:left;
           font-family: monospace;
           font-weight: 700;  
           color: #99ff99;
}}";


    private string CursorRule =>  @$".{IdFor("cursor")} {{
         display: inline-block;
         min-width: 0.7em;
         min-height: 1em;
         box-sizing: border-box;
         background: #99ff99;
         animation: {IdFor("cursor-blink")} 1s infinite;
        }}
        
        @keyframes {IdFor("cursor-blink")} {{
           0% {{opacity: 0; }}
          50% {{opacity: 0; }}
          51% {{opacity: 1; }}
          100% {{opacity:1;}}
}}";



}
