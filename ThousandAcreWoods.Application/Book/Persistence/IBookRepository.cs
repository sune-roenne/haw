using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.Application.Book.Persistence;
public interface IBookRepository
{
    Task<BookRelease> LoadBookFromInput();
    Task<BookRelease> LoadBookFromOutput();
    Task<BookRelease> UpdateOutput(BookRelease fromInput , bool allowOverwritingChanges);

}
