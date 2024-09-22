using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Story.Model;

namespace ThousandAcreWoods.Application.Story;
public interface IStoryLoader
{
    Task<Scene> LoadScene(string sceneId);

}
