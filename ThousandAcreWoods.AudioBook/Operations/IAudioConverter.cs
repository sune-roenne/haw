using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.Operations;
public interface IAudioConverter
{
    public Task<byte[]> ConvertMp3ToWav(byte[] mp3Bytes);
    public Task<byte[]> ConvertWavToMp3(byte[] wavBytes);
    public Task<byte[]> ConvertWavToM4a(byte[] wavBytes);
    public Task<byte[]> ConvertM4aToWav(byte[] m4aBytes);


}
