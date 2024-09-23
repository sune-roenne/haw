using Microsoft.Extensions.Options;
using ThousandAcreWoods.Language.Extensions;
using ThousandAcreWoods.Application.Capture.Infrastructure;
using ThousandAcreWoods.Application.Configuration;

namespace ThousandAcreWoods.Infrastructure.Capture;
internal class CaptureFileWriter : ICaptureFileWriter
{

    private readonly string _outputFolder;

    public CaptureFileWriter(IOptions<ApplicationConfiguration> conf)
    {
        _outputFolder = conf.Value.CaptureFileFolder;
    }

    public async Task WriteFile(byte[] bytes, string fileName)
    {
        var splitFileName = fileName.Split('.');
        var (fileNameStart, fileEnding) = splitFileName.Length > 1 ?
            (splitFileName.Take(splitFileName.Length - 1).MakeString("."), splitFileName[splitFileName.Length - 1]) :
            (fileName, "");
        if (!Path.Exists(_outputFolder))
            Directory.CreateDirectory(_outputFolder);
        var outputName = Path.Combine(_outputFolder, $"{fileNameStart}.{DateTime.Now.ToFileTime()}.{fileEnding}");
        await File.WriteAllBytesAsync(outputName, bytes);
    }
}
