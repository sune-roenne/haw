using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;

namespace ThousandAcreWoods.AudioBook.TextToSpeech;
public interface IAudioBookTextToSpeechClient
{
    Task<byte[]> CallWith(string SsmlDocumentString);
    

}
