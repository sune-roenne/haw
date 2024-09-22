using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.AudioOperations.Configurations;
public class AudioOperationsConfiguration
{
    public const string AudioOperationsConfigurationElementName = "Operations";

    public string SoXExecutablePath { get; set; }

    public string FfmpegExecutablePath { get; set; }

    public string TempFolder { get; set; }


}
