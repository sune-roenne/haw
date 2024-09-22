using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.Configuration;
using ThousandAcreWoods.AudioBook.Persistence.Manual;
using ThousandAcreWoods.AudioBook.Persistence.Playbook;
using ThousandAcreWoods.AudioBook.Persistence.Ssml;
using ThousandAcreWoods.AudioBook.TextToSpeech;
using ThousandAcreWoods.AudioBook.TextToSpeech.Audio;
using ThousandAcreWoods.LocalStorage.Configuration;

namespace ThousandAcreWoods.AudioBook;
public static class DependencyInjection
{
    public static IServiceCollection AddAudioBookConfiguration(this IServiceCollection services, IConfiguration conf)
    {
        services.Configure<AudioBookConfiguration>(conf.GetSection(AudioBookConfiguration.AudioBookConfigurationElementName));
        return services;
    }
    public static IServiceCollection AddAudioBook(this IServiceCollection services)
    {
        services.AddSingleton<ISsmlDataRepo, SsmlDataRepo>();
        services.AddSingleton<IManualMappingsRepository, ManualMappingsRepository>();
        services.AddScoped<IAudioBookTextToSpeechClient, AudioBookTextToSpeechClient>();
        services.AddScoped<IAudioBookCreator, AudioBookCreator>();
        services.AddScoped<IPlaybookCreator,PlaybookCreator>();
        services.AddScoped<IPlaybookRepository, PlaybookRepository>();
        services.AddScoped<IAudioCreator, AudioCreator>();
        return services;
    }


}
