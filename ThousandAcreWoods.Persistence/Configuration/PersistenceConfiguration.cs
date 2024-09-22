using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Persistence.Configuration;
public class PersistenceConfiguration
{

    public const string ConfigurationElementName = "Persistence";
    public DatabaseConfiguration ApplicationDatabase { get; set; }


}
