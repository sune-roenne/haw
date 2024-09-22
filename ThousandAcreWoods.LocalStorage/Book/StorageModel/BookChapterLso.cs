using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.LocalStorage.Book.StorageModel;
public class BookChapterLso
{
    public DateTime Created { get; set; }
    public string ContentHash { get; set; }
    public BookChapter Chapter { get; set; }
    public bool ContentHasChanged => BookHashingRules.ComputeHash(Chapter) != ContentHash;

}
