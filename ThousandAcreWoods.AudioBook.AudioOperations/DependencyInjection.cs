using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.AudioOperations.Configurations;
using ThousandAcreWoods.AudioBook.AudioOperations.Services;
using ThousandAcreWoods.AudioBook.Operations;

namespace ThousandAcreWoods.AudioBook.AudioOperations;
public static class DependencyInjection
{

    public static IServiceCollection ConfigureAudioOperations(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AudioOperationsConfiguration>(config.GetSection(AudioOperationsConfiguration.AudioOperationsConfigurationElementName));
        return services;
    }

    public static IServiceCollection AddAudioOperations(this IServiceCollection services)
    {
        services.AddSingleton<IAudioConverter, AudioConverter>();
        return services;
    }

}
