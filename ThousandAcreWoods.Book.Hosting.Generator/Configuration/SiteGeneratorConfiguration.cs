using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Book.Hosting.Generator.Configuration;
public class SiteGeneratorConfiguration
{
    public const string SiteGeneratorConfigurationElementName = "Generator";

    public string GenerationOutputDirectory { get; set; }

    public string? DefaultChapterImage { get; set; }

}
