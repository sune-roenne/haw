using ThousandAcreWoods.AudioBook.AudioOperations;
using ThousandAcreWoods.AudioBook.VoiceChanger;


namespace ThousandAcreWoods.AudioBook.Recording;

public static class DependencyInjection
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json");
        builder.Configuration.AddJsonFile("appsettings.local.json", true);
        builder.Services.ConfigureAudioOperations(builder.Configuration);
        builder.Services.ConfigureVoiceChanger(builder.Configuration);
        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAudioOperations();
        builder.Services.AddVoiceChanger();
        return builder;
    }


}
