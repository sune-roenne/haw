using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThousandAcreWoods.LocalStorage.Story.Model.ScriptPart;
internal class SubtitleJso
{
    public string Text { get; set; }
    public int? DelayBefore { get; set; }
    public int? DelayAfter { get; set; }

}
