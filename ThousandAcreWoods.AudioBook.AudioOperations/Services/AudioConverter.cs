using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.AudioOperations.Configurations;
using ThousandAcreWoods.AudioBook.Operations;

namespace ThousandAcreWoods.AudioBook.AudioOperations.Services;
internal class AudioConverter : IAudioConverter
{
    private const string Mp3 = "mp3";
    private const string Wav = "wav";
    private const string M4a = "m4a";

    private readonly AudioOperationsConfiguration _config;
    private static readonly SemaphoreSlim FileLock = new SemaphoreSlim(1,1);
    

    public AudioConverter(IOptions<AudioOperationsConfiguration> config)
    {
        _config = config.Value;
        if(!Directory.Exists(_config.TempFolder))
            Directory.CreateDirectory(_config.TempFolder);
    }

    public async Task<byte[]> ConvertMp3ToWav(byte[] mp3Bytes) => await ConvertWithSox(Mp3, Wav, mp3Bytes);

    public async Task<byte[]> ConvertWavToMp3(byte[] wavBytes) => await ConvertWithSox(Wav, Mp3, wavBytes);

    public async Task<byte[]> ConvertWavToM4a(byte[] wavBytes) => await ConvertWithFfmpeg(Wav, M4a, wavBytes);

    public async Task<byte[]> ConvertM4aToWav(byte[] m4aBytes) => await ConvertWithFfmpeg(M4a, Wav, m4aBytes);



    private async Task<byte[]> ConvertWithSox(string fromFormat, string toFormat, byte[] inputBytes) =>
        await Convert(_config.SoXExecutablePath, fromFormat, toFormat, inputBytes, (fromFile, toFile) => $"{fromFile} {toFile}");

    private async Task<byte[]> ConvertWithFfmpeg(string fromFormat, string toFormat, byte[] inputBytes) =>
        await Convert(_config.FfmpegExecutablePath, fromFormat, toFormat, inputBytes, (fromFile, toFile) => $"-i {fromFile} {toFile}");


    private async Task<byte[]> Convert(string programFile, string fromFormat, string toFormat, byte[] inputBytes, Func<string, string, string> commandCreator)
    {
        var fileId = await ReserveFileId();
        var inputFileName = $"{fileId}.{fromFormat}";
        var absoluteInputFileName = Path.Combine(_config.TempFolder, inputFileName);
        await File.WriteAllBytesAsync(absoluteInputFileName, inputBytes);
        var outputFileName = $"{fileId}.{toFormat}";
        var absoluteOutputFileName = Path.Combine(_config.TempFolder, outputFileName);
        Perform(programFile, commandCreator(inputFileName, outputFileName));
        var outputBytes = await File.ReadAllBytesAsync(Path.Combine(_config.TempFolder, outputFileName));
        File.Delete(absoluteInputFileName);
        File.Delete(absoluteOutputFileName);
        return outputBytes;
    }

    private void Perform(string programFile, string arguments)
    {
        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = programFile,
            WorkingDirectory = _config.TempFolder,
            Arguments = arguments
        };

        using var process = Process.Start(startInfo);
        process!.WaitForExit();

    }



    private static HashSet<long> ReservedFiles = new HashSet<long>();
    private static readonly Random Random = new Random();
    private async Task<long> ReserveFileId()
    {
        await FileLock.WaitAsync();
        try
        {
            var currentId = DateTime.Now.ToFileTime();
            while (ReservedFiles.Contains(currentId))
            {
                currentId += Random.NextInt64(0, long.MaxValue);
            }
            return currentId;
        }
        finally
        {
            if(ReservedFiles.Count > 1000)
            {
                ReservedFiles = ReservedFiles
                    .Order()
                    .Take(100)
                    .ToHashSet();
            }
            FileLock.Release();
        }

    }

}
