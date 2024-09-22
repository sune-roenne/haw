
using Microsoft.JSInterop;
using System.Text.Json;

namespace ThousandAcreWoods.Book.Hosting.Wasm.State;

public class LocalStorageRepository : ILocalStorageRepository
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _jsObject;
    private JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        WriteIndented = false
    };

    public LocalStorageRepository(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task Clear()
    {
        try
        {
            await WithJsRef(async _ => await _.InvokeVoidAsync("clear"));
        }
        catch (Exception) { }
    }

    public async Task Delete(string key)
    {
        try
        {
            await WithJsRef(async _ => await _.InvokeVoidAsync("get", key));
        }
        catch (Exception) { }
    }

    public async Task<TOut?> Get<TOut>(string key) where TOut : class
    {
        try
        {
            var loaded = await WithJsRef(async _ => await _.InvokeAsync<string>("get", key));
            if (loaded == null)
                return null;
            var mapped = JsonSerializer.Deserialize<TOut>(loaded);
            return mapped;
        }
        catch (Exception) { }
        return null;
    }

    public async Task Set<TIn>(string key, TIn value) where TIn : class
    {
        var serialized = JsonSerializer.Serialize(value, _serializerOptions);
        await WithJsRef(async _ => await _.InvokeVoidAsync("set", key, serialized));
    }

    private async Task WithJsRef(Func<IJSObjectReference, Task> toDo) => await WithJsRef<int>(async _ => { await toDo(_); return 0; });

    private async Task<T> WithJsRef<T>(Func<IJSObjectReference, Task<T>> toDo)
    {
        if(_jsObject == null)
        {
            _jsObject = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/LocalStorageRepository.js");
        }
        var returnee = await toDo(_jsObject);
        return returnee;
    }
}
