using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Story.Model;
public record Scene(
    string SceneId,
    string SceneName,
    SceneType SceneType,
    IReadOnlyCollection<ScriptPart> ScriptParts,
    StoryImage? Image
    );
