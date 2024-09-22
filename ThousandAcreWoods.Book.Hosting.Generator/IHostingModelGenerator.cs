using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.Book.Hosting.Generator;
internal interface IHostingModelGenerator
{
    Task GenerateSiteData(BookRelease book);
}
