using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Application.Configuration;
public class ApplicationConfiguration
{
    public const string ConfigurationElementName = "Application";

    public string AdminUsersString { get; set; }


    public IReadOnlySet<string> AdminUsersSet => AdminUsersString.Split(";")
        .Select(_ => _.Trim().ToLower())
        .ToHashSet();

    public string CaptureFileFolder { get; set; }


}
