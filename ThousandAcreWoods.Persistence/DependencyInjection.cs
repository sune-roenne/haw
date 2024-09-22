using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Persistence.Configuration;

namespace ThousandAcreWoods.Persistence;
public static class DependencyInjection
{

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration conf)
    {
        services.Configure<PersistenceConfiguration>(conf.GetSection(PersistenceConfiguration.ConfigurationElementName));



        return services;
    }

}
