
using Microsoft.JSInterop;
using System.Net.Http.Json;
using ThousandAcreWoods.Book.Hosting.Wasm.Model;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Persistence;

public class FileDownloadInitiator : IFileDownloadInitiator
{

    private readonly IJSRuntime _jsRuntime;
    private readonly IServiceScopeFactory _scopeFactory;

    public FileDownloadInitiator(IJSRuntime jsRuntime, IServiceScopeFactory scopeFactory)
    {
        _jsRuntime = jsRuntime;
        _scopeFactory = scopeFactory;
    }

    public async Task StartDownload(string resourceUri, string saveFileName)
    {
        using var scope = _scopeFactory.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
        httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        var bytes = await httpClient.GetByteArrayAsync(resourceUri);
        var streamRef = new DotNetStreamReference(new MemoryStream(bytes));
        await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", saveFileName, streamRef);
    }
}
