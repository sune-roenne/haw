using ThousandAcreWoods.AudioBook.Operations;
using ThousandAcreWoods.AudioBook.Recording;
using ThousandAcreWoods.AudioBook.Recording.Components;
using ThousandAcreWoods.AudioBook.VoiceChanger;

var builder = WebApplication.CreateBuilder(args)
    .Configure()
    .AddServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

/*var audioConverter = app.Services.GetRequiredService<IAudioConverter>();
var expFolder = "c:/temp/audiobook.recording.explorations";
var testBytes = await File.ReadAllBytesAsync($"{expFolder}/sample-recording.m4a");
var converted = await audioConverter.ConvertM4aToWav(testBytes);
var wavFileName = $"{expFolder}/sample-recording-{DateTime.Now.ToFileTime()}.wav";*/
var inputFile = $@"C:\temp\audiobook.recording.explorations\sample-recording-133689212256901117.wav";
var inputBytes = await File.ReadAllBytesAsync(inputFile);
var voiceChanger = app.Services.GetRequiredService<IOkadaVoiceChangerService>();
var changedUp = await voiceChanger.ChangeVoice(inputBytes, OkadaVoice.KikotoKurage);
var outputFile = $@"C:\temp\audiobook.recording.explorations\changed-voice-{DateTime.Now.ToFileTime()}.wav";
await File.WriteAllBytesAsync(outputFile, changedUp);


app.Run();
