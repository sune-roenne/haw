using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Audio;
public interface IAudioCreator
{
    Task<PlaybookEntryNode> CreateAudioFor(PlaybookEntryNode node, string chapterSemanticId, long timeId);

    Task<PlaybookMergedNode> CreateAudioFor(PlaybookMergedNode node, string chapterSemanticId, long timeId);

    Task<PlaybookChapter> ConcatenateAudioFiles(PlaybookChapter chapter, long timeId);
}
