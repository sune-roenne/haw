using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;
public record ManualCharacterMapping(
    string CharacterName,
    TextToSpeechVoiceDefinition Voice,
    TextToSpeechVoiceConfiguration VoiceConfiguration
    )
{
    public string CharacterKey = CharacterName.ToLower().Trim();

}
