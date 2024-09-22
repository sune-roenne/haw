using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;

namespace ThousandAcreWoods.AudioBook.TextToSpeech;
public static class SsmlConstants
{

    public static class Pauses
    {
        public static readonly SsmlPause VeryShortPause = new SsmlPause(100);
        public static readonly SsmlPause ShortPause = new SsmlPause(200);
        public static readonly SsmlPause MediumPause = new SsmlPause(500);
        public static readonly SsmlPause LongPause = new SsmlPause(1_000);
        public static readonly SsmlPause VeryLongPause = new SsmlPause(3_000);
    }

    public static class VoiceStyleIntensity
    {
        public const decimal Min = 0.01m;
        public const decimal Max = 2m;

    }

    public static class VoiceStyles
    {
        //Expresses an excited and high-energy tone for promoting a product or service.
        public const string Advertisement = "advertisement_upbeat";
        //Expresses a warm and affectionate tone, with higher pitch and vocal energy. The speaker is in a state of attracting the attention of the listener. The personality of the speaker is often endearing in nature.
        public const string Affectionate = "affectionate";
        //Expresses an angry and annoyed tone.
        public const string Angry = "angry";
        //Expresses a warm and relaxed tone for digital assistants.
        public const string Assistant = "assistant";
        //Expresses a cool, collected, and composed attitude when speaking. Tone, pitch, and prosody are more uniform compared to other types of speech.
        public const string Calm = "calm";
        //Expresses a casual and relaxed tone.
        public const string Chat = "chat";
        //Expresses a positive and happy tone.
        public const string Cheerful = "cheerful";
        //Expresses a friendly and helpful tone for customer support.
        public const string CustomerService = "customerservice";
        //Expresses a melancholic and despondent tone with lower pitch and energy.
        public const string Depressed = "depressed";
        //Expresses a disdainful and complaining tone. Speech of this emotion displays displeasure and contempt.
        public const string Disgruntled = "disgruntled";
        //Narrates documentaries in a relaxed, interested, and informative style suitable for documentaries, expert commentary, and similar content.
        public const string Documentary = "documentary-narration";
        //Expresses an uncertain and hesitant tone when the speaker is feeling uncomfortable.
        public const string Embarrassed = "embarrassed";
        //Expresses a sense of caring and understanding.
        public const string Empathetic = "empathetic";
        //Expresses a tone of admiration when you desire something that someone else has.
        public const string Envious = "envious";
        //Expresses an upbeat and hopeful tone. It sounds like something great is happening and the speaker is happy about it.
        public const string Excited = "excited";
        //Expresses a scared and nervous tone, with higher pitch, higher vocal energy, and faster rate. The speaker is in a state of tension and unease.
        public const string Fearful = "fearful";
        //Expresses a pleasant, inviting, and warm tone. It sounds sincere and caring.
        public const string Friendly = "friendly";
        //Expresses a mild, polite, and pleasant tone, with lower pitch and vocal energy.
        public const string Gentle = "gentle";
        //Expresses a warm and yearning tone. It sounds like something good will happen to the speaker.
        public const string Hopeful = "hopeful";
        //Expresses emotions in a melodic and sentimental way.
        public const string Lyrical = "lyrical";
        //Expresses a professional, objective tone for content reading.
        public const string NarrationProfessional = "narration-professional";
        //Expresses a soothing and melodious tone for content reading.
        public const string NarrationRelaxed = "narration-relaxed";
        //Expresses a formal and professional tone for narrating news.
        public const string Newscast = "newscast";
        //Expresses a versatile and casual tone for general news delivery.
        public const string NewscastCasual = "newscast-casual";
        //Expresses a formal, confident, and authoritative tone for news delivery.
        public const string NewscastFormal = "newscast-formal";
        //Expresses an emotional and rhythmic tone while reading a poem.
        public const string Poetry = "poetry-reading";
        //Expresses a sorrowful tone.
        public const string Sad = "sad";
        //Expresses a strict and commanding tone. Speaker often sounds stiffer and much less relaxed with firm cadence.
        public const string Serious = "serious";
        //Expresses a tone that sounds as if the voice is distant or in another location and making an effort to be clearly heard.
        public const string Shouting = "shouting";
        //Expresses a relaxed and interested tone for broadcasting a sports event.
        public const string Sports = "sports_commentary";
        //Expresses an intensive and energetic tone for broadcasting exciting moments in a sports event.
        public const string SportsExcited = "sports_commentary_excited";
        //Expresses a soft tone that's trying to make a quiet and gentle sound.
        public const string Whispering = "whispering";
        //Expresses a scared tone, with a faster pace and a shakier voice. It sounds like the speaker is in an unsteady and frantic status.
        public const string Terrified = "terrified";
        //Expresses a cold and indifferent tone.
        public const string Unfriendly = "unfriendly";
        //
    }

    public static class Voices
    {
        public const string NarratorMappingName = "narrator";
        public static class Men
        {
            public static readonly TextToSpeechVoiceDefinition EnAUDarren = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, DarrenNeural)",
                 DisplayName: "Darren",
                 LocalName: "Darren",
                 ShortName: "en-AU-DarrenNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 159
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUDuncan = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, DuncanNeural)",
                 DisplayName: "Duncan",
                 LocalName: "Duncan",
                 ShortName: "en-AU-DuncanNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 153
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUKen = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, KenNeural)",
                 DisplayName: "Ken",
                 LocalName: "Ken",
                 ShortName: "en-AU-KenNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 159
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUNeil = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, NeilNeural)",
                 DisplayName: "Neil",
                 LocalName: "Neil",
                 ShortName: "en-AU-NeilNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 148
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUTim = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, TimNeural)",
                 DisplayName: "Tim",
                 LocalName: "Tim",
                 ShortName: "en-AU-TimNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 144
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUWilliam = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, WilliamNeural)",
                 DisplayName: "William",
                 LocalName: "William",
                 ShortName: "en-AU-WilliamNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 157
                 );

            public static readonly TextToSpeechVoiceDefinition EnCALiam = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-CA, LiamNeural)",
                 DisplayName: "Liam",
                 LocalName: "Liam",
                 ShortName: "en-CA-LiamNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-CA",
                 LocaleName: "English (Canada)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 180
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBAlfie = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, AlfieNeural)",
                 DisplayName: "Alfie",
                 LocalName: "Alfie",
                 ShortName: "en-GB-AlfieNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 157
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBElliot = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, ElliotNeural)",
                 DisplayName: "Elliot",
                 LocalName: "Elliot",
                 ShortName: "en-GB-ElliotNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 150
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBEthan = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, EthanNeural)",
                 DisplayName: "Ethan",
                 LocalName: "Ethan",
                 ShortName: "en-GB-EthanNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 155
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBNoah = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, NoahNeural)",
                 DisplayName: "Noah",
                 LocalName: "Noah",
                 ShortName: "en-GB-NoahNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 155
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBOliver = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, OliverNeural)",
                 DisplayName: "Oliver",
                 LocalName: "Oliver",
                 ShortName: "en-GB-OliverNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 155
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBOllieMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, OllieMultilingualNeural)",
                 DisplayName: "Ollie Multilingual",
                 LocalName: "Ollie Multilingual",
                 ShortName: "en-GB-OllieMultilingualNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 24000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
     );

            public static readonly TextToSpeechVoiceDefinition EnGBRyan = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, RyanNeural)",
                 DisplayName: "Ryan",
                 LocalName: "Ryan",
                 ShortName: "en-GB-RyanNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 161
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBThomas = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, ThomasNeural)",
                 DisplayName: "Thomas",
                 LocalName: "Thomas",
                 ShortName: "en-GB-ThomasNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 154
                 );

            public static readonly TextToSpeechVoiceDefinition EnHKSam = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-HK, SamNeural)",
                 DisplayName: "Sam",
                 LocalName: "Sam",
                 ShortName: "en-HK-SamNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-HK",
                 LocaleName: "English (Hong Kong SAR)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 140
                 );

            public static readonly TextToSpeechVoiceDefinition EnIEConnor = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-IE, ConnorNeural)",
                 DisplayName: "Connor",
                 LocalName: "Connor",
                 ShortName: "en-IE-ConnorNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-IE",
                 LocaleName: "English (Ireland)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 146
                 );

            public static readonly TextToSpeechVoiceDefinition EnINPrabhat = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-IN, PrabhatNeural)",
                 DisplayName: "Prabhat",
                 LocalName: "Prabhat",
                 ShortName: "en-IN-PrabhatNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-IN",
                 LocaleName: "English (India)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 129
                 );

            public static readonly TextToSpeechVoiceDefinition EnKEChilemba = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-KE, ChilembaNeural)",
                 DisplayName: "Chilemba",
                 LocalName: "Chilemba",
                 ShortName: "en-KE-ChilembaNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-KE",
                 LocaleName: "English (Kenya)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 156
                 );

            public static readonly TextToSpeechVoiceDefinition EnNGAbeo = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-NG, AbeoNeural)",
                 DisplayName: "Abeo",
                 LocalName: "Abeo",
                 ShortName: "en-NG-AbeoNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-NG",
                 LocaleName: "English (Nigeria)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 153
                 );

            public static readonly TextToSpeechVoiceDefinition EnNZMitchell = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-NZ, MitchellNeural)",
                 DisplayName: "Mitchell",
                 LocalName: "Mitchell",
                 ShortName: "en-NZ-MitchellNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-NZ",
                 LocaleName: "English (New Zealand)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 149
                 );

            public static readonly TextToSpeechVoiceDefinition EnPHJames = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-PH, JamesNeural)",
                 DisplayName: "James",
                 LocalName: "James",
                 ShortName: "en-PH-JamesNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-PH",
                 LocaleName: "English (Philippines)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 147
                 );

            public static readonly TextToSpeechVoiceDefinition EnSGWayne = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-SG, WayneNeural)",
                 DisplayName: "Wayne",
                 LocalName: "Wayne",
                 ShortName: "en-SG-WayneNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-SG",
                 LocaleName: "English (Singapore)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 133
                 );

            public static readonly TextToSpeechVoiceDefinition EnTZElimu = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-TZ, ElimuNeural)",
                 DisplayName: "Elimu",
                 LocalName: "Elimu",
                 ShortName: "en-TZ-ElimuNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-TZ",
                 LocaleName: "English (Tanzania)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 152
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAIGenerate1 = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AIGenerate1Neural)",
                 DisplayName: "AIGenerate1",
                 LocalName: "AIGenerate1",
                 ShortName: "en-US-AIGenerate1Neural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 135
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAlloyTurboMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AlloyTurboMultilingualNeural)",
                 DisplayName: "AlloyTurboMultilingual",
                 LocalName: "AlloyTurboMultilingual",
                 ShortName: "en-US-AlloyTurboMultilingualNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAndrew = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AndrewNeural)",
                 DisplayName: "Andrew",
                 LocalName: "Andrew",
                 ShortName: "en-US-AndrewNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAndrewMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AndrewMultilingualNeural)",
                 DisplayName: "Andrew Multilingual",
                 LocalName: "Andrew Multilingual",
                 ShortName: "en-US-AndrewMultilingualNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
     );

            public static readonly TextToSpeechVoiceDefinition EnUSBrandon = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, BrandonNeural)",
                 DisplayName: "Brandon",
                 LocalName: "Brandon",
                 ShortName: "en-US-BrandonNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 156
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSBrian = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, BrianNeural)",
                 DisplayName: "Brian",
                 LocalName: "Brian",
                 ShortName: "en-US-BrianNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSBrianMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, BrianMultilingualNeural)",
                 DisplayName: "Brian Multilingual",
                 LocalName: "Brian Multilingual",
                 ShortName: "en-US-BrianMultilingualNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
     );

            public static readonly TextToSpeechVoiceDefinition EnUSChristopher = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, ChristopherNeural)",
                 DisplayName: "Christopher",
                 LocalName: "Christopher",
                 ShortName: "en-US-ChristopherNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 149
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSDavis = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, DavisNeural)",
                 DisplayName: "Davis",
                 LocalName: "Davis",
                 ShortName: "en-US-DavisNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 154
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSEric = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, EricNeural)",
                 DisplayName: "Eric",
                 LocalName: "Eric",
                 ShortName: "en-US-EricNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 147
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSGuy = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, GuyNeural)",
                 DisplayName: "Guy",
                 LocalName: "Guy",
                 ShortName: "en-US-GuyNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 215
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSJacob = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, JacobNeural)",
                 DisplayName: "Jacob",
                 LocalName: "Jacob",
                 ShortName: "en-US-JacobNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 154
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSJason = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, JasonNeural)",
                 DisplayName: "Jason",
                 LocalName: "Jason",
                 ShortName: "en-US-JasonNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 156
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSKai = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, KaiNeural)",
                 DisplayName: "Kai",
                 LocalName: "Kai",
                 ShortName: "en-US-KaiNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 24000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSRoger = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, RogerNeural)",
                 DisplayName: "Roger",
                 LocalName: "Roger",
                 ShortName: "en-US-RogerNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSRyanMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, RyanMultilingualNeural)",
                 DisplayName: "Ryan Multilingual",
                 LocalName: "Ryan Multilingual",
                 ShortName: "en-US-RyanMultilingualNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 190
     );

            public static readonly TextToSpeechVoiceDefinition EnUSSteffan = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, SteffanNeural)",
                 DisplayName: "Steffan",
                 LocalName: "Steffan",
                 ShortName: "en-US-SteffanNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 154
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSSteffanMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, SteffanMultilingualNeural)",
                 DisplayName: "Steffan Multilingual",
                 LocalName: "Steffan Multilingual",
                 ShortName: "en-US-SteffanMultilingualNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 24000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 190
     );

            public static readonly TextToSpeechVoiceDefinition EnUSTony = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, TonyNeural)",
                 DisplayName: "Tony",
                 LocalName: "Tony",
                 ShortName: "en-US-TonyNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 156
                 );

            public static readonly TextToSpeechVoiceDefinition EnZALuke = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-ZA, LukeNeural)",
                 DisplayName: "Luke",
                 LocalName: "Luke",
                 ShortName: "en-ZA-LukeNeural",
                 Gender: TextToSpeechGenderType.Male,
                 Locale: "en-ZA",
                 LocaleName: "English (South Africa)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 168
                 );


        }

        public static class Women
        {
            public static readonly TextToSpeechVoiceDefinition EnAUAnnette = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, AnnetteNeural)",
                 DisplayName: "Annette",
                 LocalName: "Annette",
                 ShortName: "en-AU-AnnetteNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 154
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUCarly = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, CarlyNeural)",
                 DisplayName: "Carly",
                 LocalName: "Carly",
                 ShortName: "en-AU-CarlyNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 137
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUElsie = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, ElsieNeural)",
                 DisplayName: "Elsie",
                 LocalName: "Elsie",
                 ShortName: "en-AU-ElsieNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 148
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUFreya = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, FreyaNeural)",
                 DisplayName: "Freya",
                 LocalName: "Freya",
                 ShortName: "en-AU-FreyaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 148
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUJoanne = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, JoanneNeural)",
                 DisplayName: "Joanne",
                 LocalName: "Joanne",
                 ShortName: "en-AU-JoanneNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 153
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUKim = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, KimNeural)",
                 DisplayName: "Kim",
                 LocalName: "Kim",
                 ShortName: "en-AU-KimNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 150
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUNatasha = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, NatashaNeural)",
                 DisplayName: "Natasha",
                 LocalName: "Natasha",
                 ShortName: "en-AU-NatashaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 139
                 );

            public static readonly TextToSpeechVoiceDefinition EnAUTina = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-AU, TinaNeural)",
                 DisplayName: "Tina",
                 LocalName: "Tina",
                 ShortName: "en-AU-TinaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-AU",
                 LocaleName: "English (Australia)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 136
                 );

            public static readonly TextToSpeechVoiceDefinition EnCAClara = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-CA, ClaraNeural)",
                 DisplayName: "Clara",
                 LocalName: "Clara",
                 ShortName: "en-CA-ClaraNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-CA",
                 LocaleName: "English (Canada)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 167
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBAbbi = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, AbbiNeural)",
                 DisplayName: "Abbi",
                 LocalName: "Abbi",
                 ShortName: "en-GB-AbbiNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 145
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBAdaMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, AdaMultilingualNeural)",
                 DisplayName: "Ada Multilingual",
                 LocalName: "Ada Multilingual",
                 ShortName: "en-GB-AdaMultilingualNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 24000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
     );

            public static readonly TextToSpeechVoiceDefinition EnGBBella = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, BellaNeural)",
                 DisplayName: "Bella",
                 LocalName: "Bella",
                 ShortName: "en-GB-BellaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 146
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBHollie = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, HollieNeural)",
                 DisplayName: "Hollie",
                 LocalName: "Hollie",
                 ShortName: "en-GB-HollieNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 144
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBLibby = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, LibbyNeural)",
                 DisplayName: "Libby",
                 LocalName: "Libby",
                 ShortName: "en-GB-LibbyNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 138
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBMaisie = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, MaisieNeural)",
                 DisplayName: "Maisie",
                 LocalName: "Maisie",
                 ShortName: "en-GB-MaisieNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 141
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBMia = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, MiaNeural)",
                 DisplayName: "Mia",
                 LocalName: "Mia",
                 ShortName: "en-GB-MiaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Deprecated,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBOlivia = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, OliviaNeural)",
                 DisplayName: "Olivia",
                 LocalName: "Olivia",
                 ShortName: "en-GB-OliviaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 143
                 );

            public static readonly TextToSpeechVoiceDefinition EnGBSonia = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-GB, SoniaNeural)",
                 DisplayName: "Sonia",
                 LocalName: "Sonia",
                 ShortName: "en-GB-SoniaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-GB",
                 LocaleName: "English (United Kingdom)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 150
                 );

            public static readonly TextToSpeechVoiceDefinition EnHKYan = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-HK, YanNeural)",
                 DisplayName: "Yan",
                 LocalName: "Yan",
                 ShortName: "en-HK-YanNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-HK",
                 LocaleName: "English (Hong Kong SAR)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 144
                 );

            public static readonly TextToSpeechVoiceDefinition EnIEEmily = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-IE, EmilyNeural)",
                 DisplayName: "Emily",
                 LocalName: "Emily",
                 ShortName: "en-IE-EmilyNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-IE",
                 LocaleName: "English (Ireland)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 135
                 );

            public static readonly TextToSpeechVoiceDefinition EnINNeerja = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-IN, NeerjaNeural)",
                 DisplayName: "Neerja",
                 LocalName: "Neerja",
                 ShortName: "en-IN-NeerjaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-IN",
                 LocaleName: "English (India)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnKEAsilia = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-KE, AsiliaNeural)",
                 DisplayName: "Asilia",
                 LocalName: "Asilia",
                 ShortName: "en-KE-AsiliaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-KE",
                 LocaleName: "English (Kenya)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 143
                 );

            public static readonly TextToSpeechVoiceDefinition EnNGEzinne = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-NG, EzinneNeural)",
                 DisplayName: "Ezinne",
                 LocalName: "Ezinne",
                 ShortName: "en-NG-EzinneNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-NG",
                 LocaleName: "English (Nigeria)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 142
                 );

            public static readonly TextToSpeechVoiceDefinition EnNZMolly = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-NZ, MollyNeural)",
                 DisplayName: "Molly",
                 LocalName: "Molly",
                 ShortName: "en-NZ-MollyNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-NZ",
                 LocaleName: "English (New Zealand)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 132
                 );

            public static readonly TextToSpeechVoiceDefinition EnPHRosa = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-PH, RosaNeural)",
                 DisplayName: "Rosa",
                 LocalName: "Rosa",
                 ShortName: "en-PH-RosaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-PH",
                 LocaleName: "English (Philippines)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 137
                 );

            public static readonly TextToSpeechVoiceDefinition EnSGLuna = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-SG, LunaNeural)",
                 DisplayName: "Luna",
                 LocalName: "Luna",
                 ShortName: "en-SG-LunaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-SG",
                 LocaleName: "English (Singapore)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 133
                 );

            public static readonly TextToSpeechVoiceDefinition EnTZImani = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-TZ, ImaniNeural)",
                 DisplayName: "Imani",
                 LocalName: "Imani",
                 ShortName: "en-TZ-ImaniNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-TZ",
                 LocaleName: "English (Tanzania)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 142
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAIGenerate2 = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AIGenerate2Neural)",
                 DisplayName: "AIGenerate2",
                 LocalName: "AIGenerate2",
                 ShortName: "en-US-AIGenerate2Neural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 140
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAmber = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AmberNeural)",
                 DisplayName: "Amber",
                 LocalName: "Amber",
                 ShortName: "en-US-AmberNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 152
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAna = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AnaNeural)",
                 DisplayName: "Ana",
                 LocalName: "Ana",
                 ShortName: "en-US-AnaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 135
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAria = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AriaNeural)",
                 DisplayName: "Aria",
                 LocalName: "Aria",
                 ShortName: "en-US-AriaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 150
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAshley = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AshleyNeural)",
                 DisplayName: "Ashley",
                 LocalName: "Ashley",
                 ShortName: "en-US-AshleyNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 149
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAva = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AvaNeural)",
                 DisplayName: "Ava",
                 LocalName: "Ava",
                 ShortName: "en-US-AvaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSAvaMultiLinguag = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, AvaMultilingualNeural)",
                 DisplayName: "Ava Multilingual",
                 LocalName: "Ava Multilingual",
                 ShortName: "en-US-AvaMultilingualNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
     );

            public static readonly TextToSpeechVoiceDefinition EnUSCora = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, CoraNeural)",
                 DisplayName: "Cora",
                 LocalName: "Cora",
                 ShortName: "en-US-CoraNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 146
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSElizabeth = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, ElizabethNeural)",
                 DisplayName: "Elizabeth",
                 LocalName: "Elizabeth",
                 ShortName: "en-US-ElizabethNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 152
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSEmma = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, EmmaNeural)",
                 DisplayName: "Emma",
                 LocalName: "Emma",
                 ShortName: "en-US-EmmaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSEmmaMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, EmmaMultilingualNeural)",
                 DisplayName: "Emma Multilingual",
                 LocalName: "Emma Multilingual",
                 ShortName: "en-US-EmmaMultilingualNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 0
     );

            public static readonly TextToSpeechVoiceDefinition EnUSEvelyn = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, EvelynMultilingualNeural)",
                 DisplayName: "Evelyn Multilingual",
                 LocalName: "Evelyn Multilingual",
                 ShortName: "en-US-EvelynMultilingualNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 24000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 190
     );

            public static readonly TextToSpeechVoiceDefinition EnUSJane = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, JaneNeural)",
                 DisplayName: "Jane",
                 LocalName: "Jane",
                 ShortName: "en-US-JaneNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 154
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSJenny = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, JennyNeural)",
                 DisplayName: "Jenny",
                 LocalName: "Jenny",
                 ShortName: "en-US-JennyNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 152
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSJennyMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, JennyMultilingualNeural)",
                 DisplayName: "Jenny Multilingual",
                 LocalName: "Jenny Multilingual",
                 ShortName: "en-US-JennyMultilingualNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 190
     );

            public static readonly TextToSpeechVoiceDefinition EnUSLuna = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, LunaNeural)",
                 DisplayName: "Luna",
                 LocalName: "Luna",
                 ShortName: "en-US-LunaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 24000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSMichelle = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, MichelleNeural)",
                 DisplayName: "Michelle",
                 LocalName: "Michelle",
                 ShortName: "en-US-MichelleNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 154
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSMonica = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, MonicaNeural)",
                 DisplayName: "Monica",
                 LocalName: "Monica",
                 ShortName: "en-US-MonicaNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 145
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSNancy = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, NancyNeural)",
                 DisplayName: "Nancy",
                 LocalName: "Nancy",
                 ShortName: "en-US-NancyNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 149
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSNovaTurboMultilingual = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, NovaTurboMultilingualNeural)",
                 DisplayName: "NovaTurboMultilingual",
                 LocalName: "NovaTurboMultilingual",
                 ShortName: "en-US-NovaTurboMultilingualNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
                 );

            public static readonly TextToSpeechVoiceDefinition EnUSSara = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, SaraNeural)",
                 DisplayName: "Sara",
                 LocalName: "Sara",
                 ShortName: "en-US-SaraNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 157
                 );

            public static readonly TextToSpeechVoiceDefinition EnZALeah = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-ZA, LeahNeural)",
                 DisplayName: "Leah",
                 LocalName: "Leah",
                 ShortName: "en-ZA-LeahNeural",
                 Gender: TextToSpeechGenderType.Female,
                 Locale: "en-ZA",
                 LocaleName: "English (South Africa)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.GA,
                 WordsPerMinute: 143
                 );

        }

        public static class Neutrals
        {
            public static readonly TextToSpeechVoiceDefinition EnUSBlue = new TextToSpeechVoiceDefinition(
                 Name: "Microsoft Server Speech Text to Speech Voice (en-US, BlueNeural)",
                 DisplayName: "Blue",
                 LocalName: "Blue",
                 ShortName: "en-US-BlueNeural",
                 Gender: TextToSpeechGenderType.Neutral,
                 Locale: "en-US",
                 LocaleName: "English (United States)",
                 SampleRateHertz: 48000,
                 VoiceType: TextToSpeechVoiceType.Neural,
                 Status: TextToSpeechVoiceStatus.Preview,
                 WordsPerMinute: 0
                 );

        }



    }





}
