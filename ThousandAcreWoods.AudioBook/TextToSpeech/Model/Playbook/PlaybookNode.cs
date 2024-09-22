using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Playbook;
public abstract record PlaybookNode(
    string? Mp3FileName
    )
{
    private static long _currentNodeId = 0;
    private static object _nodeIdLock = new { };
    private static long NextId()
    {
        lock (_nodeIdLock)
        {
            _currentNodeId += 1;
            return _currentNodeId;
        }
    }

    public readonly long UniqueNodeId = NextId();

    public abstract string EntryShaHash { get; }
    public abstract string SsmlShaHash { get; }
    public abstract string Mp3ShaHash { get; }

    public virtual int SsmlEntryCharacterCount => 0;

    public abstract string SemanticNodeId { get; }


}
