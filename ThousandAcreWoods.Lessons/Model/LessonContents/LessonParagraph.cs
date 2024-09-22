using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Lessons.Model.LessonContents;
public record LessonParagraph(
    IReadOnlyCollection<LessonParagraphContent> Content
    ) : LessonContent;
