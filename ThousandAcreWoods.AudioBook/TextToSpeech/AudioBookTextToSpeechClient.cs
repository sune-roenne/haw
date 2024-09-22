using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;

namespace ThousandAcreWoods.AudioBook.TextToSpeech;
public class AudioBookTextToSpeechClient : IAudioBookTextToSpeechClient
{
    private readonly TextToSpeechConfiguration _config;

    public AudioBookTextToSpeechClient(IOptions<AudioBookConfiguration> config)
    {
        _config = config.Value.TextToSpeech;
    }

    public async Task<byte[]> CallWith(string SsmlDocumentString)
    {
        var speechConfig = SpeechConfig.FromSubscription(_config.SpeechKey, _config.SpeechRegion);
        speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio48Khz192KBitRateMonoMp3);
        using var synth = new SpeechSynthesizer(speechConfig);
        var result = await synth.SpeakSsmlAsync(SsmlDocumentString);
        return result.AudioData;
    }

}
