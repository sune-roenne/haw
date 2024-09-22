using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.Domain.Book.Model;
public abstract record BookChapterContent()
{

    public string ShaHash() => this switch
    {
        BookChapterSection sec => sec.Title.ToShaHash(),
        BookCharacterLine lin => lin.LineParts.AggregateHash(lin.IsThought.ToString(), _ => $"{_.PartText}{_.Description ?? ""}"),
        BookCharacterStoryTime st => st.Title.ToShaHash().CombineHash(st.Story),
        BookContextBreak ct => "ContextBreak".ToShaHash(),
        BookNarration narr => narr.NarrationContent.ToShaHash(),
        BookNarrationList lis => lis.Items.AggregateHash("NarrLis".ToShaHash(), _ => _),
        BookSinging sing => sing.LinesSong.AggregateHash(sing.Character.CharacterKey, _ => _),
        _ => "Unknown".ToShaHash()
    };

    public string? CharacterAudioKey => this switch
    {
        BookCharacterLine lin => lin.Character.CharacterAudioKey,
        BookCharacterStoryTime st => st.Character.CharacterAudioKey,
        BookSinging sing => sing.Character.CharacterAudioKey,
        _ => null
    };



}
