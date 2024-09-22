using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Lessons.Model.LessonContents;

namespace ThousandAcreWoods.Lessons.Model;
public record Lesson(
    LessonHeader Header,
    IReadOnlyCollection<LessonContent> Contents
    );
