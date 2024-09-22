using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.LocalStorage.Configuration;
public class LocalStorageConfiguration
{
    public const string LocalStorageConfigurationElementName = "LocalStorage";

    public string BookStoryInputFolder { get; set; }

    public string BookStoryOutputFolder { get; set; }

}
