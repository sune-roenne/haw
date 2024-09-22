using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;
public record SsmlEntry(
    string SemanticId,
    string? CharacterKey,
    TextToSpeechVoiceDefinition Voice,
    TextToSpeechVoiceConfiguration? Configuration,
    IReadOnlyCollection<SsmlContent> Content,
    int? PauseInMillis
    )
{
    public int SsmlContentCharacterCount = Content
        .Collect(_ => _ as SsmlLine)
        .Select(_ => _.SsmlText.Length)
        .Pipe(conts => conts.Any() ? conts.Sum() : 0);

}
