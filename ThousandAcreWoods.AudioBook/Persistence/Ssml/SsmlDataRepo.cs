using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.Persistence.Playbook.Model;
using ThousandAcreWoods.AudioBook.Persistence.Ssml.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;

namespace ThousandAcreWoods.AudioBook.Persistence.Ssml;
internal class SsmlDataRepo : ISsmlDataRepo
{

    private static string DataFolder = AudioBookConfiguration.ManagedSsmlFolderAbsolutePath;
    private static string ExportDataFolder = AudioBookConfiguration.ManagedSsmlExportFolderAbsolutePath;

    private const string StorageEntryEnding = "ssml.json";
    private const string ExportEntryEnding = "ssml";

    private static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions { WriteIndented = true };

    public async Task<IReadOnlyCollection<StorageEntry<string>>> Load()
    {
        var directories = Directory.GetDirectories(DataFolder);
        var files = directories
            .SelectMany(dir => Directory.GetFiles(dir)
                .Where(_ => _.ToLower().EndsWith(StorageEntryEnding))
                .Select(filNam => (FileName: filNam, SubFolder: Path.GetRelativePath(DataFolder, dir)))
            ).ToList();
        var loadTasks = files
            .Select(async _ => (await File.ReadAllTextAsync(_.FileName)).Pipe(txt => (Text: txt, _.SubFolder)))
            .ToList();
        await Task.WhenAll(loadTasks);
        var parsedResults = loadTasks
            .Select(_ => _.Result)
            .Select(_ => (Jso: JsonSerializer.Deserialize<SsmlStorageEntryJso>(_.Text)!, _.SubFolder))
            .ToList();
        var mappedResults = parsedResults
            .Select(_ => _.Jso.ToSsml(_.SubFolder))
            .ToList();
        return mappedResults;
    }

    public async Task<StorageEntry<string>> Load(PlaybookEntryNode node)
    {
        var loaded = await File.ReadAllTextAsync(node.SsmlFileName!);
        var asJson = JsonSerializer.Deserialize<SsmlStorageEntryJso>(loaded)!;
        var subFolder = Path.GetRelativePath(DataFolder, Directory.GetParent(node.SsmlFileName!)!.FullName);
        var returnee = asJson.ToSsml(subFolder);
        return returnee;
    }


    public async Task<IReadOnlyCollection<(T Input, StorageEntry<string> StorageEntry, string FileName)>> Save<T>(IEnumerable<T> inputs, Func<T,StorageEntry<string>> mapper)
    {
        if (!inputs.Any())
            return [];
        var entries = inputs
            .Select(_ => (Input: _, AsStorage: mapper(_)))
            .Select(_ => (_.Input, _.AsStorage, SubLocation: _.AsStorage.SubLocation))
            .ToList();
        var mapped = entries
            .Select(_ => (
                _.Input, 
                _.AsStorage, 
                AsJso: _.AsStorage.ToJso(), 
                FileName: FileNameFor(_.AsStorage, _.SubLocation), 
                ExportFileName: ExportFileNameFor(_.AsStorage, _.SubLocation), 
                _.SubLocation
               )
             ).ToList();
        var asStrings = mapped
            .Select(_ => (_.Input, _.AsStorage, _.AsJso, _.FileName, _.ExportFileName, AsString: JsonSerializer.Serialize(_.AsJso, SerializerOptions), _.SubLocation))
            .ToList();

        var allSubfolders = asStrings
            .Select(_ => _.SubLocation)
            .Distinct()
            .Select(_ => $"{DataFolder}/{_}")
            .ToList();
        foreach( var subfolder in allSubfolders)
        {
            if (Directory.Exists(subfolder))
                Directory.Delete(subfolder, true);
            Directory.CreateDirectory(subfolder);
        }

        var byCounts = asStrings
            .GroupBy(_ => _.FileName)
            .Where(_ => _.Count() > 1)
            .Select(_ => (Count: _.Count(), Entries : _.ToList()))
            .OrderByDescending(_ => _.Count)
            .ToList();

        var writeTasks = asStrings
            .Select(_ => File.WriteAllTextAsync(_.FileName, _.AsString))
            .ToList();
        await Task.WhenAll(writeTasks);
        await SaveExports(asStrings
            .Where(_ => _.AsStorage.Entry.Length > 100)
            .DistinctBy(_ => _.AsStorage.SemanticId)
            .Select(_ => (_.ExportFileName, _.AsStorage.Entry, _.SubLocation)));
        var returnee = asStrings
            .Select(_ => (_.Input, _.AsStorage, _.FileName))
            .ToList();
        return returnee;
    }

    private async Task SaveExports(IEnumerable<(string FileName, string Content, string SubLocation)> toSave)
    {
        if(!toSave.Any()) return;
        var allSubLocations = toSave
            .Select(_ => _.SubLocation)
            .Distinct()
            .ToList();
        foreach (var subLoc in allSubLocations)
        {
            var folder = $"{ExportDataFolder}/{subLoc}";
            if (Directory.Exists(folder))
                Directory.Delete(folder, true);
            Directory.CreateDirectory(folder);

        }
        var saveTasks = toSave
            .Select(async inp => await File.WriteAllTextAsync(inp.FileName, inp.Content))
            .ToList();
        await Task.WhenAll(saveTasks);
    }


    private static string FileNameFor<T>(StorageEntry<T> entry, string subLocation) where T : class => 
        $"{DataFolder}/{subLocation}/{entry.OrderIndex.ToString("00000")}_{entry.SemanticId}.{StorageEntryEnding}";

    private static string ExportFileNameFor<T>(StorageEntry<T> entry, string subLocation) where T : class =>
        $"{ExportDataFolder}/{subLocation}/{entry.SemanticId}.{ExportEntryEnding}";

}
