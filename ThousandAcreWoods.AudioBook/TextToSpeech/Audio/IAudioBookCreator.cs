using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Audio;
public interface IAudioBookCreator
{
    Task<IReadOnlyCollection<PlaybookChapter>> CreateBook(BookRelease book, params int[] generateAudioForChapters);


}
