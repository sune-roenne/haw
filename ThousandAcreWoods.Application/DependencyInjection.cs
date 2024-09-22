using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Application.Configuration;
using ThousandAcreWoods.Application.Platform.Services;
using ThousandAcreWoods.Application.Workers;
using ThousandAcreWoods.Domain.Configuration;

namespace ThousandAcreWoods.Application;
public static class DependencyInjection
{


    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration conf) => services
        .Configure(conf)
        .AddServices()
        .AddHostedServices();

    private static IServiceCollection Configure(this IServiceCollection services, IConfiguration conf) => services
        .Configure<ApplicationConfiguration>(conf.GetSection(ApplicationConfiguration.ConfigurationElementName))
        .Configure<DomainConfiguration>(conf.GetSection(DomainConfiguration.ConfigurationElementName));

    private static IServiceCollection AddServices(this IServiceCollection services) => services
        .AddSingleton<IRecurringNotificationService, RecurringNotificationService>();

    private static IServiceCollection AddHostedServices(this IServiceCollection services) =>
        services.AddHostedService<RecurringNotificationWorker>();

}
