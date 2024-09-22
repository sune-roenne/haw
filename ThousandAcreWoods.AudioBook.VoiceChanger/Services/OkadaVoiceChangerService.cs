using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.VoiceChanger.Clients;
using AppConf = ThousandAcreWoods.AudioBook.VoiceChanger.Configuration.VoiceChangerConfiguration;

namespace ThousandAcreWoods.AudioBook.VoiceChanger.Services;
internal class OkadaVoiceChangerService : IOkadaVoiceChangerService
{

    private readonly IOkadaVoiceChangerClient _client;
    private readonly AppConf _conf;

    public OkadaVoiceChangerService(IOkadaVoiceChangerClient client, IOptions<AppConf> conf)
    {
        _conf = conf.Value;
        _client = client;
        if(!Directory.Exists(_conf.VoiceChangerTempFolder))
            Directory.CreateDirectory(_conf.VoiceChangerTempFolder);
        var configurable = (OkadaVoiceChangerClient)_client;
    }


    public async Task<byte[]> ChangeVoice(byte[] wavBytes, OkadaVoice okadaVoice)
    {
        var configJson = (JsonElement) await _client.Get_configuration_api_configuration_manager_configuration_get2Async(reload: true);
        var config = JsonSerializer.Deserialize<VoiceChangerConfiguration>(configJson)!;
        if(config.Current_slot_index != (int) okadaVoice)
        {
            config.Current_slot_index = (int) okadaVoice;
            config.Voice_changer_input_mode = "client";
            await _client.Put_configuration_api_configuration_manager_configuration_put2Async(config);
        }
        var fileId = await ReserveFileId();
        /*var result = await _client.Post_convert_chunk_api_voice_changer_convert_chunk_post2Async(
            x_timestamp: null,
            waveform: new FileParameter(
                data: new MemoryStream(wavBytes) { Capacity = wavBytes.Length},
                fileName: $"{fileId}.wav",
                contentType: "application/octet-stream"
            ));*/
        var result = await _client.Post_convert_file_api_voice_changer_convert_file_post2Async(new ConvertFileParam
        {
            Src_path = "133689756369439819.wav",
            Dst_path = "133689756369439819_changed.wav"
        });
        var readBytes = new byte[0];
        return readBytes;
    }



    private static readonly SemaphoreSlim FileLock = new SemaphoreSlim(1, 1);
    private static HashSet<long> ReservedFiles = new HashSet<long>();
    private static readonly Random Random = new Random();
    private async Task<long> ReserveFileId()
    {
        await FileLock.WaitAsync();
        try
        {
            var currentId = DateTime.Now.ToFileTime();
            while (ReservedFiles.Contains(currentId))
            {
                currentId += Random.NextInt64(0, long.MaxValue);
            }
            return currentId;
        }
        finally
        {
            if (ReservedFiles.Count > 1000)
            {
                ReservedFiles = ReservedFiles
                    .Order()
                    .Take(100)
                    .ToHashSet();
            }
            FileLock.Release();
        }

    }



}
