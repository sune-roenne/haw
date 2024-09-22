using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.VoiceChanger.Configuration;
public class VoiceChangerConfiguration
{
    public const string VoiceChangerConfigurationElementName = "VoiceChanger";

    public string OkadaBaseUrl { get; set; }

    public required string VoiceChangerTempFolder { get; set; }
}
