using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Book.Configuration;
public class BookConfiguration
{
    public const string BookConfigurationElementName = "Book";

    public string TemplateDirectory { get; set; }

    public string GenerationOutputDirectory { get; set; }

    public string DefaultChapterImage { get; set; }

    public string LuaLaTeXExecutablePath { get; set; }

    public string PdfOutputDir { get; set; }

    public string WasmAppPdfFolder { get; set; }

}
