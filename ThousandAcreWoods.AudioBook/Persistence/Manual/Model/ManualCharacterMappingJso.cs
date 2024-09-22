using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Persistence.Manual.Model;
using ThousandAcreWoods.AudioBook.Persistence.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Manual;

namespace ThousandAcreWoods.AudioBook.Persistence.Manual.Model;
public record ManualCharacterMappingJso(
        string CharacterName,
        string Voice,
        TextToSpeechProsodySpecificationJso? Prosody,
        string? Role,
        string? Style
    )
{
    public ManualCharacterMapping ToModel() => new ManualCharacterMapping(
        CharacterName: CharacterName,
        Voice: TextToSpeechLookups.VoiceFor(Voice),
        VoiceConfiguration: new TextToSpeechVoiceConfiguration(
            ContourChanges: Prosody?.ContourChanges?.Select(_ => _.ToModel())?.ToList(),
            PitchInHertz: Prosody?.PitchInHertz,
            PitchChangeInPercent: Prosody?.PitchChangeInPercent,
            RateInPercent: Prosody?.RateInPercent,
            Role: Role.PipeOpt(Enum.Parse<TextToSpeechRole>),
            Style: Style.PipeOpt(TextToSpeechLookups.StyleFor)
            )
        );
}

public static class TextToSpeechCharacterMappingJsoExtensions
{
    public static ManualCharacterMappingJso ToJso(this ManualCharacterMapping mapp) => new ManualCharacterMappingJso(
        CharacterName: mapp.CharacterName,
        Voice: mapp.Voice.ShortName,
        Prosody: new TextToSpeechProsodySpecificationJso(
            ContourChanges: mapp.VoiceConfiguration?.ContourChanges?.Select(_ => _.ToJso())?.ToList(),
            PitchInHertz: mapp.VoiceConfiguration?.PitchInHertz,
            PitchChangeInPercent: mapp.VoiceConfiguration?.PitchChangeInPercent,
            RateInPercent: mapp.VoiceConfiguration?.RateInPercent
            ),
        Role: mapp.VoiceConfiguration?.ToString(),
        Style: mapp.VoiceConfiguration?.Style?.ToString()
        );
}
