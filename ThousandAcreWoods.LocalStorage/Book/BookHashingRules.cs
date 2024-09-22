using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.LocalStorage.Book;
public static class BookHashingRules
{
    public static string ComputeHash(this BookChapter chapter)
    {
        var sb = new StringBuilder();
        InsertMetaData(sb,chapter);
        InsertContent(sb,chapter);
        var stringForHashing = sb.ToString();
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(stringForHashing));
        var returnee = hashedBytes
            .Select(_ => _.ToString("x2"))
            .MakeString("");
        return returnee;
    }

    private static void InsertMetaData(StringBuilder sb, BookChapter chap)
    {
        sb.Append(chap.ChapterName);
        sb.Append(chap.ChapterDate.ToString("yyyy-MM-dd"));
        sb.Append(chap.ChapterOrder);
        if (chap.MetaData == null)
            return;
        if (chap.MetaData.ChapterDate != null)
            sb.Append(chap.MetaData.ChapterDate.Value.ToString("yyyy-MM-dd"));
        if(chap.MetaData.ChapterTitle != null)
            sb.Append(chap.MetaData.ChapterTitle);
        if (chap.MetaData.StoryLineKey != null)
            sb.Append(chap.MetaData.StoryLineKey);
        if (chap.MetaData.Aliases != null)
            foreach (var pair in chap.MetaData.Aliases.DistinctBy(_ => _.Key.ToLower().Trim()).OrderBy(_ => _.Key.ToLower().Trim()))
                sb.Append($"{pair.Key}={pair.Value}");
        if (chap.MetaData.ChapterOrder != null)
            sb.Append(chap.MetaData.ChapterOrder);
    }

    private static void InsertContent(StringBuilder sb, BookChapter chap)
    {
        foreach(var cont in chap.ChapterContents)
        {
            if (cont is BookNarration narr)
                sb.Append(narr.NarrationContent);
            else if(cont is BookCharacterLine lin)
            {
                sb.Append(lin.Character.CharacterKey);
                sb.Append(lin.IsThought.ToString());
                foreach(var part in lin.LineParts)
                {
                    sb.Append($"{part.PartText}{part.Description ?? ""}");
                }
            }
        }
    }

}


