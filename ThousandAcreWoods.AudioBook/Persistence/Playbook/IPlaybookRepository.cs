using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
using ThousandAcreWoods.Domain.Book.Model;

namespace ThousandAcreWoods.AudioBook.Persistence.Playbook;
public interface IPlaybookRepository
{
    Task<PlaybookChapter> SaveChapter(PlaybookChapter chapter, BookChapter bookChapter);

    Task<IReadOnlyCollection<PlaybookChapter>> LoadChapters();



}
