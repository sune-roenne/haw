using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Application.Book.Persistence;
using ThousandAcreWoods.Application.Story;
using ThousandAcreWoods.LocalStorage.Book;
using ThousandAcreWoods.LocalStorage.Configuration;
using ThousandAcreWoods.LocalStorage.Story;

namespace ThousandAcreWoods.LocalStorage;
 public static class DependencyInjection
{

    public static IServiceCollection AddLocalStorageConfiguration(this IServiceCollection services, IConfiguration conf)
    {
        services.Configure<LocalStorageConfiguration>(conf.GetSection(LocalStorageConfiguration.LocalStorageConfigurationElementName));
        return services;
    }


    public static IServiceCollection AddLocalStorage(this IServiceCollection services)
    {

        services.AddSingleton<IStoryLoader, StoryLoader>();
        services.AddTransient<IBookRepository, LocalStorageBookRepository>();
        return services;
    }

}
