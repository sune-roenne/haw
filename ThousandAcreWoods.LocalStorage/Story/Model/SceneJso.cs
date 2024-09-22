using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.LocalStorage.Story.Model.ScriptPart;

namespace ThousandAcreWoods.LocalStorage.Story.Model;
internal class SceneJso
{
    public string SceneId { get; set; }
    public string SceneName { get; set; }
    public string? ImageName { get; set; }
    public IReadOnlyCollection<ScriptPartJso> Script { get; set; }

}
