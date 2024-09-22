using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.Configuration;
public class AudioBookConfiguration
{
    public const string AudioBookConfigurationElementName = "AudioBook";

    public const string ManagedDataFolderRelativePath = "data/managed";
    public const string ManualDataFolderRelativePath = "data/manual";


    public static string ManagedSsmlFolderRelativePath => $"{ManagedDataFolderRelativePath}/ssml";

    private static string AudioBookDefaultFolder => $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}/../../..";

    public static string ManagedDataFolderAbsolutePath => $"{AudioBookDefaultFolder}/{ManagedDataFolderRelativePath}";
    public static string ManagedSsmlFolderAbsolutePath => $"{AudioBookDefaultFolder}/{ManagedSsmlFolderRelativePath}";
    public static string ManagedSsmlExportFolderAbsolutePath => $"{ManagedSsmlFolderAbsolutePath}/exports";
    public static string ManagedMp3FolderAbsolutePath => $"{ManagedDataFolderAbsolutePath}/mp3";


    public static string ManualDataFolderAbsolutePath => $"{AudioBookDefaultFolder}/{ManualDataFolderRelativePath}";


    public static string CharacterMappingFileName => $"{ManualDataFolderAbsolutePath}/character-map.json";

    public static string AliasMappingFileName => $"{ManualDataFolderAbsolutePath}/character-alias-map.json";


    public TextToSpeechConfiguration TextToSpeech { get; set; }
    public AudioConfiguration Audio { get; set; }

    /// <summary>
    /// Should point to git clone directory for project: https://github.com/anars/blank-audio
    /// </summary>

    public string SilenceFilesDirectory { get; set; }

}
