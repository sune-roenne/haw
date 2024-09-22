using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;

namespace ThousandAcreWoods.AudioBook.Persistence.TextToSpeech.Model;
public record TextToSpeechContourChangeJso(
    decimal ProgressInPercent,
    decimal ChangeInHertz
    )
{
    public TextToSpeechContourChange ToModel() => new TextToSpeechContourChange(
        ProgressInPercent: ProgressInPercent,
        ChangeInHertz: ChangeInHertz
        );
}


public static class TextToSpeechContourChangeJsoExtensions
{
    public static TextToSpeechContourChangeJso ToJso(this TextToSpeechContourChange chan) => new TextToSpeechContourChangeJso(
        ProgressInPercent: chan.ProgressInPercent,
        ChangeInHertz: chan.ChangeInHertz
        );
}