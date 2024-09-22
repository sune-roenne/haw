using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.Persistence.Ssml;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Audio;
internal class AudioCreator : IAudioCreator
{
    private readonly IReadOnlyCollection<SilenceFile> _silenceFiles;


    private readonly ILogger<AudioCreator> _logger;
    private readonly ISsmlDataRepo _dataRepo;
    private readonly IAudioBookTextToSpeechClient _speechClient;
    private readonly AudioBookConfiguration _config;

    private static long _currentId = 0;
    private static object _idLock = new object();
    private static long NextId()
    {
        lock(_idLock)
        {
            _currentId += 1;
            return _currentId;
        }
    }

    public AudioCreator(ILogger<AudioCreator> logger, ISsmlDataRepo dataRepo, IAudioBookTextToSpeechClient speechClient, IOptions<AudioBookConfiguration> config)
    {

        _config = config.Value;
        _logger = logger;
        _dataRepo = dataRepo;
        _speechClient = speechClient;
        _silenceFiles = LoadSilenceFiles();
    }



    public async Task<PlaybookEntryNode> CreateAudioFor(PlaybookEntryNode node, string chapterSemanticId, long timeId)
    {
        if (await FileIsAlreadyOk(node.Mp3FileName, node.Mp3ContentShaHash))
            return node;
        if(node.SsmlFileName == null || !File.Exists(node.SsmlFileName))
        {
            _logger.LogError($"Was asked to create audio file for: {node.SsmlFileName} but it doesn't exist");
            return node;
        }
        if(node.PauseInMillis != null)
        {
            var pauseFile = LoadSilenceFileFor(node.PauseInMillis.Value);
            _logger.LogInformation($"Using pause file: {pauseFile.FileName} for node: {node.SemanticId} of pause: {node.PauseInMillis}");
            node = node with
            {
                Mp3FileName = pauseFile.FileName,
                Mp3ContentShaHash = pauseFile.ShaHash
            };
            return node;
        }

        var ssmlEntry = await _dataRepo.Load(node);
        var createdBytes = await _speechClient.CallWith(ssmlEntry.Entry);
        var outputFolder = OutputFolderFor(timeId, chapterSemanticId);
        if(!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        var outputFileName = FileNameFor(outputFolder, node.SemanticId);
        await File.WriteAllBytesAsync(outputFileName, createdBytes);

        if(createdBytes.Length < 5)
        {
            var replacementFile = LoadSilenceFileFor(milliSeconds: 500);
            _logger.LogInformation($"Generating mp3 for {node.SemanticId} was not a success, so inserting pause instead");
            _logger.LogInformation($"{ssmlEntry.Entry}");
            node = node with
            {
                Mp3FileName = replacementFile.FileName,
                Mp3ContentShaHash = replacementFile.ShaHash
            };
            return node;
        }

        node = node with
        {
            Mp3FileName = outputFileName,
            Mp3ContentShaHash = createdBytes.ToShaHash()
        };
        return node;
    }

    public async Task<PlaybookMergedNode> CreateAudioFor(PlaybookMergedNode node, string chapterSemanticId, long timeId)
    {
        if (await FileIsAlreadyOk(node.Mp3FileName, node.Mp3ContentShaHash))
            return node;

        var subNodeTasks = node.EntryNodes
            .Select(async node => await CreateAudioFor(node, chapterSemanticId, timeId))
            .ToList();
        await Task.WhenAll(subNodeTasks);
        var subNodes = subNodeTasks.Select(_ => _.Result).ToList();
        var toMerge = subNodes
            .Select(_ => _.Mp3FileName!)
            .ToList();
        var outputFolder = OutputFolderFor(timeId, chapterSemanticId);
        var outputFileName = FileNameFor(outputFolder, node.SemanticId);
        MergeAudioFiles(toMerge, outputFileName);
        var createdBytes = await File.ReadAllBytesAsync(outputFileName);
        var mp3ByteHash = createdBytes.ToShaHash();
        node = node with
        {
            EntryNodes = subNodes,
            Mp3FileName = outputFileName,
            Mp3ContentShaHash = mp3ByteHash
        };
        return node;
    }

    private async Task<bool> FileIsAlreadyOk(string? fileName, string? fileHash)
    {
        if (fileName == null || fileHash == null)
            return false;
        if(!File.Exists(fileName)) 
            return false;
        var read = await File.ReadAllBytesAsync(fileName);
        var calcHash = read.ToShaHash();
        if (calcHash == fileHash)
            return true;
        return false;

    }

    public void ConcatenateAudioFiles(IEnumerable<string> filenames, string outputFileName, string workingDir)
    {
        if (!filenames.Any())
            throw new Exception("You don't dun give me no files man!");

        filenames = filenames
            .Select(nam => Path.GetRelativePath(workingDir, nam))
            .ToList();

        var arguments = $"{filenames.Select(fil => $@"""{fil}""").MakeString(" ")} \"{outputFileName}\"";
        _logger.LogInformation($"Using arguments string: {arguments}");

        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = _config.Audio.SoXExecutablePath,
            WorkingDirectory = workingDir,
            Arguments = arguments
        };

        using var process = Process.Start(startInfo);
        process!.WaitForExit();

    }


    public void MergeAudioFiles(IEnumerable<string> filenames, string outputFileName)
    {
        if (!filenames.Any())
            throw new Exception("You don't dun give me no files man!");

        var startInfo = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = _config.Audio.SoXExecutablePath,
            Arguments = $"-m {filenames.Select(fil => $@"""{fil}""").MakeString(" ")} \"{outputFileName}\""
        };

        using var process = Process.Start(startInfo);
        process!.WaitForExit();
    }

    private static string OutputFolderFor(long timeId, string chapterSemanticId) => $"{AudioBookConfiguration.ManagedMp3FolderAbsolutePath}/{timeId}/{chapterSemanticId}";

    private static string FileNameFor(string outputFolder, string semanticId) => $"{outputFolder}/{semanticId}_{NextId()}.mp3";

    public async Task<PlaybookChapter> ConcatenateAudioFiles(PlaybookChapter chapter, long timeId)
    {
        var toConcat = chapter
            .Nodes
            .Where(_ => _.Mp3FileName != null && File.Exists(_.Mp3FileName))
            .Select(_ => _.Mp3FileName!)
            .ToList();
        if (!toConcat.Any())
            return chapter;

        var outputFileName = $"{AudioBookConfiguration.ManagedMp3FolderAbsolutePath}/{chapter.ChapterIndex}_{timeId}_{chapter.ChapterSemanticId}.mp3";
        ConcatenateAudioFiles(toConcat, outputFileName, AudioBookConfiguration.ManagedMp3FolderAbsolutePath);

        await Task.CompletedTask;

        chapter = chapter with
        {
            Mp3FileName = outputFileName
        };
        return chapter;
    }

    private SilenceFile LoadSilenceFileFor(int milliSeconds) => _silenceFiles
        .OrderBy(fil => (Math.Abs(milliSeconds - fil.Milliseconds), fil.Milliseconds - milliSeconds))
        .First();
        


    private  IReadOnlyCollection<SilenceFile> LoadSilenceFiles()
    {
        var allMp3Files = Directory.GetFiles(_config.SilenceFilesDirectory)
            .Select(_ => _.ToLower())
            .Where(_ => _.EndsWith("mp3"))
            .Select(filNam => new SilenceFile(
               FileName: filNam,
               Milliseconds: filNam switch
               {
                   _ when filNam.Contains("250-milli") => 250,
                   _ when filNam.Contains("500-milli") => 500,
                   _ when filNam.Contains("750-milli") => 750,
                   _ when filNam.Contains("1-second-of") => 1_000,
                   _ when filNam.Contains("1-second-and-500") => 1_500,
                   _ when filNam.Contains("2-seconds-of") => 2_000,
                   _ when filNam.Contains("2-seconds-and-500") => 2_500,
                   _ when filNam.Contains("5-second") => 5_000,
                   _ => int.MaxValue
               },
               ShaHash: File.ReadAllBytes(filNam).ToShaHash()
            ))
            .ToList();
        return allMp3Files;
    }

    private record SilenceFile(string FileName, int Milliseconds, string ShaHash);


}
