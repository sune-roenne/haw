using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model;
public record TextToSpeechVoiceConfiguration(
    IReadOnlyCollection<TextToSpeechContourChange>? ContourChanges = null,
    decimal? PitchInHertz = null,
    decimal? PitchChangeInPercent = null,
    decimal? RateInPercent = null,
    TextToSpeechRole? Role = null,
    string? Style = null
    )
{

}
