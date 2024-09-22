using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.Application.Story;
using ThousandAcreWoods.Domain.Story.Model;
using ThousandAcreWoods.LocalStorage.Story.Model;
using ThousandAcreWoods.LocalStorage.Story.Model.Mappers;
using ThousandAcreWoods.LocalStorage.Story.Model.ScriptPart;

namespace ThousandAcreWoods.LocalStorage.Story;
internal class StoryLoader : IStoryLoader
{
    private const string ScenesFolderName = "Data/Story/Scenes";
    private const string CharacterMapFileName = "Data/Global/CharacterMap.json";
    private const string ImageMapFileName = "Data/Global/ImageMap.json";
    private readonly string CharacterMapFileNameFull = Path.Combine(ExecutingFolder, CharacterMapFileName);
    private readonly string ImageMapFileNameFull = Path.Combine(ExecutingFolder, ImageMapFileName);
    private static string ScenesFolder = Path.Combine(ExecutingFolder, ScenesFolderName);

    private readonly Dictionary<string, CacheRecord<Scene>> _parsedFiles = new Dictionary<string, CacheRecord<Scene>>();
    private CacheRecord<Dictionary<string, StoryCharacter>>? _characters;
    private CacheRecord<Dictionary<string, StoryImage>>? _images;


    private static IReadOnlyCollection<string> SceneFiles => Directory.EnumerateFiles(ScenesFolder).ToList();

    private static string ExecutingFolder => Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName;

    private static string FullName(string fileName) => $"{ScenesFolder}/{fileName}";

    public async Task<Scene> LoadScene(string sceneId)
    {
        if (!_parsedFiles.ContainsKey(sceneId))
        {
            foreach (var sceneFile in SceneFiles)
            {
                try
                {
                    var parsed = await ParseFile(sceneFile);
                    _parsedFiles[sceneId] = parsed;
                    if (parsed.Entry.SceneId == sceneId)
                    {
                        return parsed.Entry;
                    }

                }
                catch { }
            }
        }
        if (!_parsedFiles.ContainsKey(sceneId))
            throw new Exception($"Unable to find scene: {sceneId}");
        var parseResult = _parsedFiles[sceneId];
        if (parseResult.LastFileUpdateTime == File.GetLastWriteTime(parseResult.FileName).ToFileTime())
            return parseResult.Entry;
        parseResult = await ParseFile(parseResult.FileName);
        _parsedFiles[sceneId] = parseResult;
        return parseResult.Entry;
    }

    private async Task<CacheRecord<Scene>> ParseFile(string fileName)
    {
        var fileContent = await File.ReadAllBytesAsync(fileName);
        var parsed = ParseBytes(fileContent);
        var mapped = parsed!.ToDomain(CharacterMap, ImageMap);
        var fileChangeTime = File.GetLastWriteTime(fileName);
        var sceneParseRecord = new CacheRecord<Scene>(mapped, fileName, fileChangeTime.ToFileTime());
        return sceneParseRecord;
    }

    private SceneJso? ParseBytes(byte[] bytes)
    {
        try
        {
            var reader = new Utf8JsonReader(bytes);
            reader.Read();

            var (sceneId, sceneName, script) = ((string?)null, (string?)null, (IReadOnlyCollection<ScriptPartJso>?)null);
            while (sceneId == null || sceneName == null || script == null)
            {
                reader.Read();
                var propName = reader.GetString()!.ToLower().Trim();
                reader.Read();
                if (propName == "sceneid")
                    sceneId = reader.GetString()!;
                else if (propName == "scenename")
                    sceneName = reader.GetString()!;
                else if (propName == "script")
                    script = ParseScriptParts(ref reader);
            }
            return new SceneJso { SceneId = sceneId, SceneName = sceneName, Script = script };


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return null;
    }



    private IReadOnlyCollection<ScriptPartJso> ParseScriptParts(ref Utf8JsonReader reader)
    {
        var returnee = new List<ScriptPartJso>();

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            var parsed = ParseScriptPart(ref reader);
            returnee.Add(parsed);
            reader.Read();
        }
        return returnee;

    }

    private ScriptPartJso ParseScriptPart(ref Utf8JsonReader reader)
    {
        ScriptPartJso? returnee = null;
        while (reader.TokenType != JsonTokenType.StartObject)
            reader.Read();
        string? character = null;
        List<SubtitleJso>? subtitles = null;
        SubtitleJso? singleSubtitle = null;

        while (returnee == null)
        {
            while (reader.TokenType != JsonTokenType.PropertyName)
                reader.Read();
            var propName = reader.GetString()!.ToLower();
            if (propName == "character")
            {
                reader.Read();
                character = reader.GetString();
            }
            else if (propName == "subtitles")
            {
                while (reader.TokenType != JsonTokenType.StartArray)
                    reader.Read();
                subtitles = new List<SubtitleJso>();
                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    var subtitle = ParseSubtitleJso(ref reader);
                    subtitles.Add(subtitle);
                    reader.Read();
                }
            }
            else if (propName == "subtitle")
            {
                singleSubtitle = ParseSubtitleJso(ref reader);
            }
            if (character != null)
            {
                if (subtitles != null)
                    returnee = new MultiSubtitleScriptPartJso { Character = character, Subtitles = subtitles };
                else if (singleSubtitle != null)
                    returnee = new SingleSubtitleScriptPartJso { Character = character, Subtitle = singleSubtitle };
            }

        }
        while (reader.TokenType != JsonTokenType.EndObject)
            reader.Read();
        return returnee;
    }

    private SubtitleJso ParseSubtitleJso(ref Utf8JsonReader reader)
    {
        while (reader.TokenType != JsonTokenType.StartObject && reader.TokenType != JsonTokenType.String)
            reader.Read();
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            var (text, delayBefore, delayAfter) = ((string?)null, (int?)null, (int?)null);
            while (reader.TokenType != JsonTokenType.EndObject)
            {
                var propertyName = "";
                reader.Read();
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;
                if (reader.TokenType == JsonTokenType.PropertyName)
                    propertyName = reader.GetString()!.ToLower().Trim();
                reader.Read();
                if (propertyName == "text")
                    text = reader.GetString();
                else if (propertyName == "delaybefore")
                    delayBefore = reader.GetInt32();
                else if (propertyName == "delayafter")
                    delayAfter = reader.GetInt32();
            }
            return new SubtitleJso { Text = text ?? "", DelayBefore = delayBefore, DelayAfter = delayAfter };
        }
        var singleText = reader.GetString()!;
        return new SubtitleJso { Text = singleText };

    }


    private Dictionary<string, StoryCharacter> CharacterMap  =>
            CachedContent<Dictionary<string, StoryCharacter>, IReadOnlyCollection<CharacterJso>>(
                CharacterMapFileNameFull,
                ref _characters,
                _ => _.ToDictionarySafe(_ => _.Id, _ => _.ToDomain()));

    private Dictionary<string, StoryImage> ImageMap =>
            CachedContent<Dictionary<string, StoryImage>, IReadOnlyCollection<ImageJso>>(
                ImageMapFileName,
                ref _images,
                _ => _.ToDictionarySafe(_ => _.Id, _ => _.ToDomain()));



    private TOut CachedContent<TOut, TInterim>(string path, ref CacheRecord<TOut>? cacheReference, Func<TInterim, TOut> converter)
    {
        var currentFileTime = File.GetLastWriteTime(path).ToFileTime();
        if (_characters == null || _characters.LastFileUpdateTime != currentFileTime)
        {
            var mapFileContent = File.ReadAllText(path);
            var parsed = JsonSerializer.Deserialize<TInterim>(mapFileContent);
            var converted = converter(parsed!);
            var cacheRecord = new CacheRecord<TOut>(converted, path, currentFileTime);
            cacheReference = cacheRecord;
        }
        return cacheReference!.Entry;

    }

    private record CacheRecord<T>(T Entry, string FileName, long LastFileUpdateTime);

}
