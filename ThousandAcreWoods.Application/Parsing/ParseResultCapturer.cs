using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Application.Parsing;

public interface IParseResultCapturer
{
    bool IsDefinedForMultiple();
}

public abstract record ParseResultCapturer<TRes> : IParseResultCapturer
{
    public abstract ParseResult<TRes>? ParseForSingle(string text, int startIndex);
    public abstract IReadOnlyCollection<ParseResult<TRes>>? ParseForMultiple(string text, int startIndex);
    public abstract bool IsDefinedForMultiple();

    protected static bool IsWhiteSpace(char ch) => ch == ' ' || ch == '\t';
    protected static bool IsWhiteSpaceOrNewLine(char ch) => ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n';


    protected void ForwardPastSpaces(ref int currentIndex, string input, bool alsoForwardPastNewLine = false)
    {
        while (currentIndex < input.Length)
        {
            if (alsoForwardPastNewLine)
            {
                if (IsWhiteSpaceOrNewLine(input[currentIndex]))
                {
                    currentIndex += 1;
                    continue;
                }
            }
            else if (IsWhiteSpace(input[currentIndex]))
            {
                currentIndex += 1;
                continue;
            }
            break;
        }
        if (currentIndex > input.Length)
            currentIndex = input.Length;
    }

    protected string Capture(string inp, int startIndex, int endIndex) => (startIndex, endIndex) switch
    {
        _ when startIndex >= endIndex => "",
        _ when endIndex >= inp.Length => inp.Substring(startIndex),
        _ => inp.Substring(startIndex, endIndex - startIndex)
    };

}



public record JsonSingleResultCapturer<TRes>(
    ) : ParseResultCapturer<TRes>
{
    public override bool IsDefinedForMultiple() => false;
    public override IReadOnlyCollection<ParseResult<TRes>>? ParseForMultiple(string text, int startIndex) => null;
    public override ParseResult<TRes>? ParseForSingle(string text, int startIndex)
    {
        var currentIndex = startIndex;
        ForwardPastSpaces(ref currentIndex, text, alsoForwardPastNewLine: true);
        var startOfMatch = currentIndex;
        if (text[currentIndex] != '{')
            return null;
        var jsonLevel = 1;
        var returneeString = new StringBuilder();
        while(currentIndex < text.Length)
        {
            var startOfLine = currentIndex;
            var nextNewLine = text.IndexOf('\n', currentIndex);
            if(nextNewLine < 0)
                nextNewLine = text.Length - 1;
            for(; currentIndex < nextNewLine;  currentIndex++)
            {
                if (text[currentIndex] == '{')
                    jsonLevel++;
                if (text[currentIndex] == '}')
                    jsonLevel--;
                if(jsonLevel == 0)
                {
                    returneeString.AppendLine(Capture(text, startOfLine, currentIndex));
                    try
                    {
                        var result = JsonSerializer.Deserialize<TRes>(returneeString.ToString());
                        if (result != null)
                            return new ParseResult<TRes>(text, result, currentIndex, returneeString.ToString());
                    }
                    catch { }
                    return null;
                }
            }
            returneeString.AppendLine(Capture(text, startOfLine, nextNewLine));
            currentIndex = nextNewLine + 1;

        }
        return null;


    }
}

public record RegexSingleResultCapturer<TRes>(
    Regex Regex,
    Func<string, TRes> Converter
    ) : ParseResultCapturer<TRes>
{
    public override bool IsDefinedForMultiple() => false;

    public override IReadOnlyCollection<ParseResult<TRes>>? ParseForMultiple(string text, int startIndex) => null;

    public override ParseResult<TRes>? ParseForSingle(string text, int startIndex) => null;
}