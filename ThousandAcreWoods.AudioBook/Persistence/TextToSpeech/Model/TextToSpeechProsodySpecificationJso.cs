using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.Persistence.TextToSpeech.Model;
public record TextToSpeechProsodySpecificationJso(
    IReadOnlyCollection<TextToSpeechContourChangeJso>? ContourChanges = null,
    decimal? PitchInHertz = null,
    decimal? PitchChangeInPercent = null,
    decimal? RateInPercent = null
    );
