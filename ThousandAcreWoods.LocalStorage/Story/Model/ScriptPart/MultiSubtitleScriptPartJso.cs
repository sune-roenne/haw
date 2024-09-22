using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.LocalStorage.Story.Model.ScriptPart;
internal class MultiSubtitleScriptPartJso : ScriptPartJso
{
    public IReadOnlyCollection<SubtitleJso> Subtitles { get; set; }

}

