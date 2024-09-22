using Microsoft.AspNetCore.Authentication;
using NYK.Identity.UI;
using ThousandAcreWoods.LocalStorage.Configuration;
using ThousandAcreWoods.UI.APIs;
using ThousandAcreWoods.UI.Configuration;
using ThousandAcreWoods.UI.Middleware;
using ThousandAcreWoods.UI.Pages.Common;
using ThousandAcreWoods.UI.Security;

namespace ThousandAcreWoods.UI;

public static class DependencyInjection
{


    public static WebApplicationBuilder AddUi(this WebApplicationBuilder builder) =>
        builder
           .AddServices()
           .AddSecurity()
           .AddWorkers()
           .AddHostingSetup();

    private static WebApplicationBuilder AddHostingSetup(this WebApplicationBuilder builder)
    {
        var useIIs = builder.Configuration.GetValue<bool>($"{ServiceConfiguration.ConfigurationElementName}:{nameof(ServiceConfiguration.UseIIS)}");
        if (useIIs)
        {
            builder.WebHost
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIIS();
        }

        return builder;
    }


    private static WebApplicationBuilder AddSecurity(this WebApplicationBuilder builder)
    {
        builder.AddDefaultNykreditIdentitySetupForUiApp();

        /*builder.Services.AddScoped<IClaimsTransformation, UserClaimsTransformer>();
        builder.Services.AddAuthorization(authSettings =>
        {
            authSettings.AddMemberPolicy();
            authSettings.AddAdminPolicy();

        });*/
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddHttpContextAccessor();

        return builder;
    }

    private static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        return builder;
    }

    private static WebApplicationBuilder AddWorkers(this WebApplicationBuilder builder)
    {
        return builder;
    }


    public static WebApplicationBuilder AddConfigurationFiles(this WebApplicationBuilder builder)
    {
        var conf = builder.Configuration;
        builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);
        return builder;
    }

    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services
             .Configure<ServiceConfiguration>(builder.Configuration.GetSection(ServiceConfiguration.ConfigurationElementName))
             .Configure<LocalStorageConfiguration>(builder.Configuration.GetSection(LocalStorageConfiguration.LocalStorageConfigurationElementName));
        return builder;
    }



    public static WebApplication UseUi(this WebApplication app, IConfiguration conf)
    {

        app.UseNykreditOpenApiUIWithSecurity<App>();

        app.UseCaptureApi();
        /*app.UseUserApi()
           .UseTrackApi()
           .UseTopTrackRanking()
           .UseMortys()
           .UseMusicEvents();*/

        return app;
    }



}
