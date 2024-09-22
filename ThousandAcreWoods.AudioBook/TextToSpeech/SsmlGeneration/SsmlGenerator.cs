using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;
using ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
public static class SsmlGenerator
{

 
    internal static StorageEntry<string> ToSsmlStorageEntry(
        this SsmlEntry entry, 
        int index, 
        TextToSpeechVoiceDefinition narrator, 
        IEnumerable<string> casedWords, 
        string chapterId)
    {
        var hashValue = entry.Hash().ToBase64();
        var asSsmlString = entry.BuildSsmlString(narrator, casedWords);
        var returnee = new StorageEntry<string>(
            SemanticId: entry.SemanticId,
            Entry: asSsmlString,
            ShaHash: hashValue,
            OrderIndex: index,
            SubLocation: chapterId
            );
        return returnee;
    }

    private static string BuildSsmlString(this SsmlEntry entry, TextToSpeechVoiceDefinition narrator, IEnumerable<string> casedWords)
    {
        var buildState = SsmlGenerationState.Start()
            .WithVoice(entry.Voice);
        foreach(var cont in entry.Content)
        {
            if(cont is SsmlLine lin)
            {
                if(lin.SpeakerNameAnnounce != null)
                {
                    buildState = buildState
                        .WithVoice(narrator)
                        .WithParagraph()
                        .WithSentence(lin.SpeakerNameAnnounce.ToSsmlText(casedWords))
                        .WithClosedOpens();
                }
                if(lin.SsmlText.Replace(".", "").Length < 3)
                {
                    buildState = buildState
                        .WithPause(SsmlConstants.Pauses.MediumPause.DurationInMilliseconds)
                        .WithClosedOpens();
                }
                else
                {
                    var voiceConfig = lin.Configuration ?? entry.Configuration;

                    buildState = buildState
                        .WithVoice(entry.Voice)
                        .WithExpressAs(voiceConfig)
                        .WithParagraph()
                        .WithProsody(voiceConfig)
                        .WithSentence(lin.SsmlText.ToSsmlText(casedWords))
                        .WithClosedOpens();
                    if (lin.Description != null)
                    {
                        buildState = buildState
                            .WithVoice(narrator)
                            .WithParagraph()
                            .WithSentence(lin.Description.ToSsmlText(casedWords))
                            .WithClosedOpens();
                    }
                }
            }
            else if(cont is SsmlPause paus)
            {
                buildState
                    .WithParagraph()
                    .WithPause(paus.DurationInMilliseconds);
            }
        }


        var returnee = buildState.Complete();
        return returnee;
    }




    private static IReadOnlyCollection<(string RegexString, Regex Regex, Func<Match, string> Replacer)> SsmlReplacers = new List<(string RegexString, Func<Match, string> Replacer, RegexOptions? Options)>
    {
        (@"\.{2,}", (match) => SsmlConstants.Pauses.ShortPause.ToSsmlString(), null ),
        (@"('| |[0-9]|:|,)*[A-Z]{2,}([A-Z]|'| |[0-9]|:|-|,|\?)*", (match) => $"<emphasis level=\"strong\">{match.Value.ToLower()}</emphasis>", null),
        (@"a+nge+laa+",_ => "Angela", RegexOptions.IgnoreCase),
        (@"AA+R+G+H+",_ => "argh", RegexOptions.IgnoreCase),
        (@"oo+h+",_ => "ooh", RegexOptions.IgnoreCase),
        (@"we+lll+",_ => "well", RegexOptions.IgnoreCase),
        (@"joo+hn",_ => "John", RegexOptions.IgnoreCase),
        (@"baa+b+y+",_ => "baby", RegexOptions.IgnoreCase),
        (@"aa+r+k+w+a+r+d+",_ => "awkward", RegexOptions.IgnoreCase),
        (@"sii+l+e+n+c+e+",_ => "awkward", RegexOptions.IgnoreCase),
        (@"sune",_ => "<phoneme alphabet=\"ipa\" ph=\"sʊʊnɛ\">Sune</phoneme>", RegexOptions.IgnoreCase),

        ("¤", (_) => "No. ", null)
    }.Select(_ => (_.RegexString, new Regex(_.RegexString, _.Options ?? RegexOptions.None), _.Replacer))
    .ToList();

    private static string ToSsmlString(this SsmlPause paus) => SsmlGenerationPauseElement.BreakTagOf(paus.DurationInMilliseconds);

    private static IReadOnlyCollection<Func<string, string>> CaseReplacersFor(IEnumerable<string> casedWords) => casedWords
        .Select(word => (Word: word, Regex: new Regex($@"(?<=( |,|\.|')){word}(?=( |,|\.|'|:|;|\?|!))", RegexOptions.IgnoreCase)))
        .Select(_ => ReplaceForWordPair(_.Regex, _.Word))
        .ToList();

    private static Func<string, string> ReplaceForWordPair(Regex regex, string replacement) => (string str) => 
       regex.Replace(input: str, 
           evaluator: 
           (match) => 
           replacement );

    internal static string ToSsmlText(this string str, IEnumerable<string> casedWords)
    {
        foreach (var (_, regex, replacer) in SsmlReplacers)
        {
            var matches = regex.Matches(str);
            if (!matches.Any())
                continue;
            var replacements = matches
                .Select(replacer)
                .ToList();
            var splitByRegex = regex.Split(str)
                .Where(_ => _.Length > 1)
                .ToList();
            str = splitByRegex
                .Interleave(replacements)
                .MakeString("");

        }

        if(str.Contains("angela")) 
        {
            var tess = "";
        }

        var casedWordReplacers = CaseReplacersFor(casedWords);
        foreach(var rep in  casedWordReplacers)
        {
            str = rep(str);
        }

        return str;
    }



}
