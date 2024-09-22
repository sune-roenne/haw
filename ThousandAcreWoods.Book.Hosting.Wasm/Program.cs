using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ThousandAcreWoods.Book.Hosting.Wasm;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IChapterLoader, ChapterLoader>();
builder.Services.AddSingleton<ILocalStorageRepository, LocalStorageRepository>();
builder.Services.AddSingleton<IFileDownloadInitiator, FileDownloadInitiator>();

await builder.Build()
    .RunAsync();
