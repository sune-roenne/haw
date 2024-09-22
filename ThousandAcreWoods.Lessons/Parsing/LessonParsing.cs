using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Lessons.Model;

namespace ThousandAcreWoods.Lessons.Parsing;
public static class LessonParsing
{

    public static async Task<Lesson> Parse(string filename)
    {
        var fileContent = await File.ReadAllTextAsync(filename);


        throw new NotImplementedException();
    }


}
