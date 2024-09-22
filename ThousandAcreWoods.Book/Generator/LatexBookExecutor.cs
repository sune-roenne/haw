using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Book.Configuration;

namespace ThousandAcreWoods.Book.Generator;
public static class LatexBookExecutor
{

    public static async Task<string> GeneratePdfFile(BookConfiguration conf, string mainTexFile, string? suffix = null)
    {
        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = conf.LuaLaTeXExecutablePath,
            WorkingDirectory = Path.GetDirectoryName(mainTexFile),
            Arguments = $"--pdf --engine=luahbtex --synctex=1 --clean {mainTexFile}"
        };

        using var process = Process.Start(startInfo);
        process!.WaitForExit();
        var directoryOfInput = Path.GetDirectoryName(mainTexFile);
        var texFileName = Path.GetFileName(mainTexFile);
        var pdfFileName = mainTexFile.ToLower().Replace(".tex", ".pdf");
        var pdfFileNameExEnding = Path.GetFileNameWithoutExtension(pdfFileName);
        var copiedFileName = Path.Combine(conf.PdfOutputDir, pdfFileNameExEnding + (suffix.PipeOpt(_ => $"_{suffix}")) + "_" + DateTime.Now.ToFileTime() + ".pdf");
        var pdfFileBytes = await File.ReadAllBytesAsync(pdfFileName);
        await File.WriteAllBytesAsync(copiedFileName, pdfFileBytes);
        var wasmPdfFileName = Path.Combine(conf.WasmAppPdfFolder, pdfFileNameExEnding + (suffix.PipeOpt(_ => $"_{_}") ?? "") + ".pdf");
        if(File.Exists(wasmPdfFileName))
            File.Delete(wasmPdfFileName);
        await File.WriteAllBytesAsync(wasmPdfFileName, pdfFileBytes);
        return copiedFileName;
    }

}
