using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.VoiceChanger;
public interface IOkadaVoiceChangerService
{
    public Task<byte[]> ChangeVoice(byte[] wavBytes, OkadaVoice voice);


}
