using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThousandAcreWoods.LocalStorage.Story.Conversion;
public static class ScriptConverter
{
    private static readonly Regex _wordRegex = new Regex("[a-z]+", RegexOptions.IgnoreCase);

    public static async Task Process()
    {

        //var inputFile = @"c:\git\haw-story\Book\2024-03-26-Introducing John.story";
    }


    public static async Task<int> CountWords(string folder)
    {
        var storyFiles = await LoadStoryFiles(folder);
        var wordCounts = storyFiles
            .Select(_ => (File: _, Count: CountWords(_)))
            .OrderByDescending(_ => _.Count)
            .ToList();

        var wordCount = wordCounts
            .Sum(_ => _.Count);
        var subFolders = Directory.GetDirectories(folder);
        var subFoldersCounts = await Task.WhenAll(subFolders
            .Select(CountWords)
            .ToList());

        var subFolderTotalCount = subFoldersCounts
            .Sum();
        return wordCount + subFolderTotalCount;
    }

    public static int CountWords(StoryFile file) => _wordRegex.Matches(file.Content).Count;


    public static async Task<IReadOnlyCollection<StoryFile>> LoadStoryFiles(string folder)
    {
        var tasks = Directory.GetFiles(folder)
            .Where(_ => _.ToLower().EndsWith(".story"))
            .Select(async fil => new StoryFile(fil, await File.ReadAllTextAsync(fil)))
            .ToList();
        var returnee = await Task.WhenAll(tasks);
        return returnee;
    }



    public record StoryFile(string FileName, string Content);


}
