using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Runtime.ConstrainedExecution;

namespace ThousandAcreWoods.UI.Components.AnimatedFonts;

public static class AnimatedLetterRenderer
{

    public static RenderFragment RenderFragmentFrom(string text, Func<char, AnimatedLetter?> fontFamily, int containerWidthInPixels, int height, AnimatedLetter? letterOnMissing = null) => builder =>
    {
        letterOnMissing ??= new SPACE();
        var currentDelay = 0L;
        var mappedLines = Map(text, fontFamily, letterOnMissing, containerWidthInPixels);
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "container-fluid");
        foreach(var line in mappedLines)
        {
            builder.OpenComponent(2, typeof(AnimatedLetterLine));
            var lineWordsFragment = RenderFragmentLineFrom(line, height, currentDelay);
            currentDelay += line.Sum(_ => _.MillisecondsToRender);
            builder.AddAttribute(3, nameof(AnimatedLetterLine.Characters), lineWordsFragment);
            builder.CloseComponent();
        }
        builder.CloseElement();

    };

    private static RenderFragment RenderFragmentLineFrom(IReadOnlyCollection<AnimatedLetter> word, int height, long currentDelayInMillis)
    {
        var delayInMillis = currentDelayInMillis;

        var returnee = (RenderFragment) ((RenderTreeBuilder builder) => {
            foreach (var ch in word)
            {
                builder.OpenComponent(0, ch.GetType());
                builder.AddAttribute(1, nameof(AnimatedLetter.InitialDelayInMilliseconds), delayInMillis);
                builder.AddAttribute(2, nameof(AnimatedLetter.Height), height);
                builder.CloseComponent();
                delayInMillis += ch.MillisecondsToRender;
            }
        });
        currentDelayInMillis = delayInMillis;
        return returnee;

    }




    private static IReadOnlyCollection<IReadOnlyCollection<AnimatedLetter>> Map(string text, Func<char, AnimatedLetter?> fontFamily, AnimatedLetter letterOnMissing, int containerWidthInPixels)
    {
        var returnee = new List<IReadOnlyCollection<AnimatedLetter>>();
        letterOnMissing ??= new SPACE();

        var lines = text
            .Split('\r')
            .SelectMany(_ => _.Split("\n"))
            .Select(line => line
               .Split(' ')
               .Select(_ => _.Trim())
               .Where(_ => _.Length > 0)
               .ToList()
            ).ToList();
            


        foreach(var line in lines)
        {
            var currentLine = new List<AnimatedLetter>();
            var currentLineWidth = 0;
            foreach (var word in line)
            {
                var mappedWord = word
                    .Select(c => Map(c, fontFamily, letterOnMissing))
                    .Append(new SPACE())
                    .ToList();
                var lineWidthWithWord = currentLineWidth + mappedWord.Sum(_ => _.Width);
                if (lineWidthWithWord > containerWidthInPixels && currentLine.Count > 0)
                {
                    returnee.Add(currentLine);
                    currentLine = new List<AnimatedLetter>();
                    currentLineWidth = 0;
                }
                currentLine.AddRange(mappedWord);
                currentLineWidth += mappedWord.Sum(_ => _.Width);
            }
            if(currentLine.Count > 0)
               returnee.Add(currentLine);
        }


        return returnee;
    }

    private static AnimatedLetter Map(char ch, Func<char, AnimatedLetter?> fontFamily, AnimatedLetter letterOnMissing) =>
        fontFamily(ch) ?? letterOnMissing;


}
