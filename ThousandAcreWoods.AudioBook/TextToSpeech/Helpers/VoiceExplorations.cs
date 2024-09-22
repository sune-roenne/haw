using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Helpers;
internal static class VoiceExplorations
{


    public static async Task ExplorErrors(this IServiceProvider serviceProvider)
    {
        var t2sclient = serviceProvider.GetRequiredService<IAudioBookTextToSpeechClient>();

        var semanticId = "errs";
        var outputFolder = $"c:/temp2/{semanticId}";
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        var ssml = SsmlSamples.FinetuningSsml;

        var outputFileName = $"{outputFolder}/{semanticId}.{DateTime.Now.ToFileTime()}.mp3";
        var output = await t2sclient.CallWith(ssml);
        File.WriteAllBytes(outputFileName, output);


    }


public static async Task AdjustSsmlGeneration(this IServiceProvider serviceProvider)
    {
        var t2sclient = serviceProvider.GetRequiredService<IAudioBookTextToSpeechClient>();

        var semanticId = "2024041angelaGood";
        var outputFolder = $"c:/temp2/{semanticId}";
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        var ssml = SsmlSamples.FinetuningSsml;

        var outputFileName = $"{outputFolder}/{semanticId}.{DateTime.Now.ToFileTime()}.mp3";
        var output = await t2sclient.CallWith(ssml);
        File.WriteAllBytes(outputFileName, output);


    }



    public static async Task ExploreJohnVoices(this IServiceProvider serviceProvider)
    {
        var t2sclient = serviceProvider.GetRequiredService<IAudioBookTextToSpeechClient>();

        var semanticId = "202400603johnYeah";
        var outputFolder = $"c:/temp2/{semanticId}";
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        foreach (var voice in PossibleMen)
        {
            var outputFileName = $"{outputFolder}/{voice.ShortName}.mp3";
            if (File.Exists(outputFileName))
                continue;  //File.Delete(outputFileName);
            var sampleSsml = SsmlSamples.JohnRecruitsAlbert(voice);
            var output = await t2sclient.CallWith(sampleSsml);
            File.WriteAllBytes(outputFileName, output);

        }
    }


    public static async Task ExploreAngelaVoices(this IServiceProvider serviceProvider)
    {
        var t2sclient = serviceProvider.GetRequiredService<IAudioBookTextToSpeechClient>();

        var semanticId = "2024041angelaGood";
        var outputFolder = $"c:/temp2/{semanticId}";
        if(!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);
        foreach(var voice in PossibleWomen)
        {
            var outputFileName = $"{outputFolder}/{voice.ShortName}.mp3";
            if (File.Exists(outputFileName))
                continue;  //File.Delete(outputFileName);
            var sampleSsml = SsmlSamples.AngelaEatsCake(voice);
            var output = await t2sclient.CallWith(sampleSsml);
            File.WriteAllBytes(outputFileName, output);

        }


    }



    static async Task ExploreClient(IServiceProvider serviceProvider)
    {
        var t2sclient = serviceProvider.GetRequiredService<IAudioBookTextToSpeechClient>();

        var sampleSsml =
        @"
<speak xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"" version=""1.0"" xml:lang=""en-US"">
  <voice name=""en-SG-WayneNeural"" >
    <p><s>Angela closes her eyes and looks concentrated, and then a loud scream is heard from the other end of the train</s>
	</p>
  </voice>
</speak>
";

        var output = await t2sclient.CallWith(sampleSsml);
        File.WriteAllBytes($"c:/temp2/sample-output-{DateTime.Now.ToFileTime()}.mp3", output);

        /*var saveResult = await ssmlRepo.Save(allSsmlEntries, _ => _.SsmlEntry.ToSsmlStorageEntry(_.Index));

        logger.LogInformation("Save results: ");
        foreach(var res in saveResult)
        {
            logger.LogInformation($"  {res.FileName}: {res.Input.SsmlEntry.SemanticId}");
        }

        await Task.Delay(3_000);*/

        /*var reloaded = await ssmlRepo.Load();
        logger.LogInformation("Reload results: ");

        var outputDir = "c:/temp2/ssmlexports";
        if(Directory.Exists(outputDir))
           Directory.Delete(outputDir, true);
        Directory.CreateDirectory(outputDir);

        foreach (var res in reloaded.Where(_ => _.SemanticId.ToLower().StartsWith("20240410angela")))
        {
            var outputFile = $"{outputDir}/{res.SemanticId}.ssml";
            File.WriteAllText(outputFile, res.Entry);
        }

        await Task.Delay(10_000);
        */


    }


    private static class SsmlSamples
    {

        public static string JohnRecruitsAlbert(TextToSpeechVoiceDefinition voice) =>
            $@"
<speak xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"" version=""1.0"" xml:lang=""en-US"">
  
	<voice name=""{voice.ShortName}"">
		<p><s>Yeah, and if you eat it everyday of your life, then one day</s>
			<s>- the day you<emphasis level=""strong"">die</emphasis>-</s>
			<s>you're going to look back on a life-full of eating broccoli and wonder:</s>
			<s>'Was that all there was?</s>
			<s>Should I maybe have joined a cool special ops team instead of eating<emphasis level=""strong"">FUCKING BROCCOLI</emphasis>!?'</s>
			<s>But by then, it will be too late!</s></p>
	</voice>
</speak>

";


        public static string AngelaEatsCake(TextToSpeechVoiceDefinition voice) =>
            $@"

<speak xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"" version=""1.0"" xml:lang=""en-US"">
  
	<voice name=""{voice.ShortName}"">
		<p><s>Good riddance! I can't stand people - or brains - who think they can have it both ways</s>
			<s>Have your cake and eat it too<break time=""200ms""/></s>
			<s>Now, that gives me an idea<break time=""200ms""/></s>
			<s>I will close my eyes, and think about a chocolate cake with chocolate frosting<break time=""200ms""/></s>
			<s>It worked! But look at that thing, not even in my dreams could I win a baking show, huh?</s>
			<s>But<break time=""200ms""/>who cares? This is about the science of it<break time=""200ms""/></s>
		</p>
          <mstts:express-as style=""sports_commentary_excited"" styledegree=""1.5"">			
		  <p>
			<s><emphasis level=""strong"">TONIGHT LADIES AND GENTLEMAN, WE WILL ANSWER THE AGE-OLD QUESTION:</emphasis></s>
			<s><emphasis level=""strong"">'IF YOU EAT AN ENTIRE CHOCOLATE CAKE IN YOUR DREAMS, WILL YOU WAKE UP AND BE FAT?'</emphasis></s>
			<s><break time=""200ms""/><emphasis level=""strong"">AND</emphasis><emphasis level=""strong"">TO HELP US ANSWER THAT QUESTION, HERE IS THE LOVELY AAAAANGEEELLAAAAAA</emphasis></s>
			<s><break time=""200ms""/><emphasis level=""strong"">AND NOW</emphasis><break time=""200ms""/><emphasis level=""strong"">IF THE AUDIENCE WOULD PLEASE SHUT THE FUCK UP AS LOVELY ANGELA ATTEMPTS TO CRAM DOWN THIS WHOLE CAKE IN ONE GO</emphasis></s>
			<s>Drum-roll please<break time=""200ms""/></s>
			<s><break time=""200ms""/></s>
			<s><emphasis level=""strong"">AND THERE YOU HAVE IT LADIES AND GENTLEMEN, SHE'S DONE IT</emphasis><emphasis level=""strong"">LET'S HEAR FROM THE LOVELY ANGELA HOW SHE'S DOING</emphasis></s>
			<s><emphasis level=""strong"">ANGELA</emphasis><emphasis level=""strong"">HOW DO YOU FEEL RIGHT NOW?</emphasis></s>
		  </p>
          </mstts:express-as>			
		  <p>
			<s>Well Mike, I feel like I have just swallowed an entire chocolate cake<break time=""200ms""/></s>
			<s>And I don't even like cake<break time=""200ms""/>boo hoo for me<break time=""200ms""/></s>
			<s>I think I'm going to be sick!</s>
			<s><emphasis level=""strong"">ISN'T SHE JUST A TREAT LADES AND GENTLEMEN?</emphasis></s>
			<s><emphasis level=""strong"">WELL, THAT'S IT FOR TONIGHT, BE SURE TO CHECK IN AGAIN WHEN WE WAKE UP, TO DISCOVER IF 'LOVELY ANGELA' IS NOW 'FAT ANGELA'</emphasis></s>
			<s><emphasis level=""strong"">UNTIL NEXT TIME</emphasis></s>
		  </p>
	</voice>
</speak>






";

        public const string FinetuningSsml = $@"
<speak xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"" version=""1.0"" xml:lang=""en-US"">
  <voice name=""en-AU-TinaNeural"">
    <mstts:express-as style=""sports_commentary_excited"" styledegree=""2"">
        <p>
          <s><emphasis level=""strong"">'if you eat an entire chocolate cake in your dreams, will you wake up and be fat?'</emphasis></s>
          <s><break time=""200ms"" /><emphasis level=""strong"">and</emphasis><emphasis level=""strong""> to help us answer that question, here is the lovely Angela</emphasis></s>
        </p>
    </mstts:express-as>
  </voice>
  <voice name=""en-US-AndrewNeural"">
    <s>clapping</s>
  </voice>
  <voice name=""en-AU-TinaNeural"">
    <p>
      <s><break time=""200ms"" /><emphasis level=""strong"">and now</emphasis><break time=""200ms"" /><emphasis level=""strong""> if the audience would please shut the fuck up as lovely Angela attempts to cram down this whole cake in one go</emphasis></s>
      <s>Drum-roll please<break time=""200ms"" /></s>
    </p>
  </voice>
</speak>
";


        public const string ErrorExploriationSsml = @"
<speak xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"" version=""1.0"">
  <voice name=""en-US-AndrewNeural"">
      Carol
  </voice>
  <voice name=""en-US-AndrewNeural"">
      smiling
  </voice>
</speak>
";

    }


    private static IReadOnlyCollection<TextToSpeechVoiceDefinition> PossibleMen => [
    SsmlConstants.Voices.Men.EnAUDarren,
        SsmlConstants.Voices.Men.EnAUDuncan,
        SsmlConstants.Voices.Men.EnAUKen,
        SsmlConstants.Voices.Men.EnAUNeil,
        SsmlConstants.Voices.Men.EnAUTim,
        SsmlConstants.Voices.Men.EnAUWilliam,
        SsmlConstants.Voices.Men.EnGBAlfie,
        SsmlConstants.Voices.Men.EnGBElliot,
        SsmlConstants.Voices.Men.EnGBEthan,
        SsmlConstants.Voices.Men.EnGBNoah,
        SsmlConstants.Voices.Men.EnGBOliver,
        SsmlConstants.Voices.Men.EnGBRyan,
        SsmlConstants.Voices.Men.EnGBThomas,
        SsmlConstants.Voices.Men.EnIEConnor,
        SsmlConstants.Voices.Men.EnNZMitchell,
        SsmlConstants.Voices.Men.EnUSAndrew,
        SsmlConstants.Voices.Men.EnUSBrandon,
        SsmlConstants.Voices.Men.EnUSBrian,
        SsmlConstants.Voices.Men.EnUSChristopher,
        SsmlConstants.Voices.Men.EnUSDavis,
        SsmlConstants.Voices.Men.EnUSEric,
        SsmlConstants.Voices.Men.EnUSGuy,
        SsmlConstants.Voices.Men.EnUSJacob,
        SsmlConstants.Voices.Men.EnUSJason,
        SsmlConstants.Voices.Men.EnUSKai,
        SsmlConstants.Voices.Men.EnUSRoger,
        SsmlConstants.Voices.Men.EnUSSteffan,
        SsmlConstants.Voices.Men.EnUSTony
    ];

    private static IReadOnlyCollection<TextToSpeechVoiceDefinition> PossibleWomen => [
        SsmlConstants.Voices.Women.EnAUAnnette,
        SsmlConstants.Voices.Women.EnAUCarly,
        SsmlConstants.Voices.Women.EnAUElsie,
        SsmlConstants.Voices.Women.EnAUFreya,
        SsmlConstants.Voices.Women.EnAUJoanne,
        SsmlConstants.Voices.Women.EnAUKim,
        SsmlConstants.Voices.Women.EnAUNatasha,
        SsmlConstants.Voices.Women.EnAUTina,
        SsmlConstants.Voices.Women.EnGBAbbi,
        SsmlConstants.Voices.Women.EnGBBella,
        SsmlConstants.Voices.Women.EnGBHollie,
        SsmlConstants.Voices.Women.EnGBLibby,
        SsmlConstants.Voices.Women.EnGBMaisie,
        SsmlConstants.Voices.Women.EnGBMia,
        SsmlConstants.Voices.Women.EnGBOlivia,
        SsmlConstants.Voices.Women.EnGBSonia,
        SsmlConstants.Voices.Women.EnIEEmily,
        SsmlConstants.Voices.Women.EnNZMolly,
        SsmlConstants.Voices.Women.EnUSAmber,
        SsmlConstants.Voices.Women.EnUSAna,
        SsmlConstants.Voices.Women.EnUSAria,
        SsmlConstants.Voices.Women.EnUSAshley,
        SsmlConstants.Voices.Women.EnUSAva,
        SsmlConstants.Voices.Women.EnUSCora,
        SsmlConstants.Voices.Women.EnUSElizabeth,
        SsmlConstants.Voices.Women.EnUSEmma,
        SsmlConstants.Voices.Women.EnUSEvelyn,
        SsmlConstants.Voices.Women.EnUSJane,
        SsmlConstants.Voices.Women.EnUSJenny,
        SsmlConstants.Voices.Women.EnUSLuna,
        SsmlConstants.Voices.Women.EnUSMichelle,
        SsmlConstants.Voices.Women.EnUSMonica,
        SsmlConstants.Voices.Women.EnUSNancy,
        SsmlConstants.Voices.Women.EnUSSara

    ];




}
