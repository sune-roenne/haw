using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Application.Capture.Infrastructure;
using ThousandAcreWoods.Infrastructure.Capture;
using ThousandAcreWoods.Infrastructure.Configuration;

namespace ThousandAcreWoods.Infrastructure;
public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration conf)
    {
        services.Configure<InfrastructureConfiguration>(conf.GetSection(InfrastructureConfiguration.ConfigurationElementName));
        services.AddScoped<ICaptureFileWriter, CaptureFileWriter>();
        return services;
    }

}
