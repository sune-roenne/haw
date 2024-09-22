using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Story.Model;
public abstract record SubtitlePart() {
    public abstract string SubtitleText { get; }
    public virtual int DelayInMillisBefore => 0;
    public virtual int DelayInMillisAfter => 0;


    
};

public abstract record PlainTextSubtitlePart(string Text) : SubtitlePart()
{
    public override string SubtitleText => Text;
}

public record PlainSubtitlePart(string Text) : PlainTextSubtitlePart(Text);
public record StrongSubtitlePart(string Text, int DelayMillisBefore = 600, int DelayMillisAfter = 0) : PlainTextSubtitlePart(Text)
{
    public override int DelayInMillisBefore => DelayMillisBefore;
    public override int DelayInMillisAfter => DelayMillisAfter;
};

public record CharacterAttributeReferenceSubtitlePart(StoryCharacter Character, string AttributeName) : SubtitlePart() 
{
    private string? _text;
    public override string SubtitleText { get
        {
            _text ??= ExtractText();
            return _text;
        }}

    private string ExtractText()
    {
        var typ = typeof(StoryCharacter);
        var returnee = typ.GetProperty(AttributeName)!.GetValue(Character)?.ToString() ?? "";
        return returnee;

    }

};

