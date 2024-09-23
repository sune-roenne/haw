using ThousandAcreWoods.Language.Extensions;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model;
public record TextToSpeechVoiceDefinition(
    string Name,
    string DisplayName,
    string LocalName,
    string ShortName,
    TextToSpeechGenderType Gender,
    string Locale,
    string LocaleName,
    int SampleRateHertz,
    TextToSpeechVoiceType VoiceType,
    TextToSpeechVoiceStatus Status,
    int? WordsPerMinute
    )
{


}


public record TextToSpeechVoiceDefinitionPso(
    string Name,
    string DisplayName,
    string LocalName,
    string ShortName,
    string Gender,
    string Locale,
    string LocaleName,
    string SampleRateHertz,
    string VoiceType,
    string Status,
    string? WordsPerMinute
    )
{

    public TextToSpeechVoiceDefinition ToModel() => new TextToSpeechVoiceDefinition(
        Name: Name,
        DisplayName: DisplayName,
        LocalName: LocalName,
        ShortName: ShortName,
        Gender: Enum.Parse<TextToSpeechGenderType>(Gender),
        Locale: Locale,
        LocaleName: LocaleName,
        SampleRateHertz: int.Parse(SampleRateHertz),
        VoiceType: Enum.Parse<TextToSpeechVoiceType>(VoiceType),
        Status: Enum.Parse<TextToSpeechVoiceStatus>(Status),
        WordsPerMinute: WordsPerMinute.PipeOpt(int.Parse)
    );

    public TextToSpeechGenderType GenderType = Enum.Parse<TextToSpeechGenderType>(Gender);

}

