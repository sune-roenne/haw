using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Story.Model;
public abstract record ScriptPart(
    ScriptPartType ScriptPartType
    );

public abstract record SubtitleScriptPart(
    StoryCharacter? CharacterOpt,
    IReadOnlyCollection<Subtitle> Subtitles,
    ScriptPartType ScriptPartType
    ) : ScriptPart(ScriptPartType);

public record SpeachSubtitleScriptPart(
    StoryCharacter Character,
    IReadOnlyCollection<Subtitle> Subtitles
    ) : SubtitleScriptPart(Character, Subtitles, ScriptPartType.Speach);

public record ThoughtSubtitleScriptPart(
    StoryCharacter Character,
    IReadOnlyCollection<Subtitle> Subtitles
    ) : SubtitleScriptPart(Character, Subtitles, ScriptPartType.Thought);


public record NarrationSubtitleScriptPart(
    IReadOnlyCollection<Subtitle> Subtitles
    ) : SubtitleScriptPart(null, Subtitles, ScriptPartType.Narration);



