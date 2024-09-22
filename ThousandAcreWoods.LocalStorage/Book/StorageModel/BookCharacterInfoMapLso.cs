using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.LocalStorage.Book.StorageModel;
public class BookCharacterInfoMapLso
{
    public IReadOnlyCollection<BookCharacterInfo> Characters { get; set; }


}
