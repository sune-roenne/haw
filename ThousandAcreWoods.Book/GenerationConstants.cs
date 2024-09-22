using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Book;
public static class GenerationConstants
{
    public static class Files
    {
        public const string MainTexFileName = "AngelaAndJohnForever.tex";
        public const string FrontPageTexFileName = "FrontPage.tex";
    }

    public static class Tags
    {
        public const string AuthorTag = "[AUTHORNAME]";
        public const string VersionTag = "[BOOKVERSION]";
        public const string ChapterTemplateStartTag = "%CHAPTERTEMPLATESTART";
        public const string ChapterTemplateEndTag = "%CHAPTERTEMPLATEEND";
        public const string ChapterTitleTag = "[CHAPTERTITLE]";
        public const string ChapterDateTag = "[CHAPTERDATE]";
        public const string ChapterImageTag = "[CHAPTERIMAGE]";
        public const string ColorInsertTag = "%COLORINSERT";
        public const string CharacterCommandInsertTag = "%CHARACTERCOMMANDINSERT";
        public const string AboutTheAutherInsertTag = "%ABOUTTHEAUTHOR";
    }





}
