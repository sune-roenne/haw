using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.AudioBook.VoiceChanger.Clients;
using ThousandAcreWoods.AudioBook.VoiceChanger.Configuration;
using ThousandAcreWoods.AudioBook.VoiceChanger.Services;
using AppConf = ThousandAcreWoods.AudioBook.VoiceChanger.Configuration.VoiceChangerConfiguration;

namespace ThousandAcreWoods.AudioBook.VoiceChanger;
public static class DependencyInjection
{

    public static IServiceCollection ConfigureVoiceChanger(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppConf>(config.GetSection(AppConf.VoiceChangerConfigurationElementName));
        return services;
    }

    public static IServiceCollection AddVoiceChanger(this IServiceCollection services)
    {
        services.AddSingleton<IOkadaVoiceChangerService, OkadaVoiceChangerService>();
        services.AddHttpClients();
        return services;
    }


    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IOkadaVoiceChangerClient, OkadaVoiceChangerClient>(configureClient: (prov, client) =>
        {
            var config = prov.GetRequiredService<IOptions<AppConf>>().Value;
            client.BaseAddress = new Uri(config.OkadaBaseUrl);
        });
        return services;

    }



}
