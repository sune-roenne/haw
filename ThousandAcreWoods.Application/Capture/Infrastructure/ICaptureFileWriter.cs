using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Application.Capture.Infrastructure;
public interface ICaptureFileWriter
{

    Task WriteFile(byte[] bytes, string fileName);

}
