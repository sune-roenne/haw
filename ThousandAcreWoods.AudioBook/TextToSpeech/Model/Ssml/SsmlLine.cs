using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;

public record SsmlLine(
    string SsmlText,
    TextToSpeechVoiceConfiguration? Configuration,
    string? SpeakerNameAnnounce,
    string? Description = null
    ) : SsmlContent();

