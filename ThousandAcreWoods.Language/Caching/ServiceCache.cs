
namespace ThousandAcreWoods.Language.Caching;
public class ServiceCache<KeyType, IdType, ContentType> where KeyType : notnull where IdType : notnull where ContentType : notnull
{
    private readonly Dictionary<KeyType, CacheEntry<ContentType>> _cache = new();
    private readonly Func<IEnumerable<KeyType>, Task<IReadOnlyCollection<ContentType>>> _loadByKey;
    private readonly Func<IEnumerable<IdType>, Task<IReadOnlyCollection<ContentType>>> _loadById;
    private readonly Func<ContentType, KeyType> _keyExtractor;
    private readonly Func<ContentType, IdType> _idExtractor;
    private readonly Func<ContentType, DateTime> _entryTimeExtractor;
    private readonly Func<ContentType, bool> _entryIsValidForCache;

    private readonly Func<Task<IReadOnlyCollection<ContentType>>>? _initialLoadProducer;
    private readonly TaskCompletionSource _initialLoadTaskCompletionSource = new TaskCompletionSource();
    private readonly SemaphoreSlim _updateLock = new(1,1);
    private readonly Action<string,Exception?>? _errorLogger;


    public ServiceCache(
        Func<IEnumerable<KeyType>, Task<IReadOnlyCollection<ContentType>>> loadByKey,
        Func<IEnumerable<IdType>, Task<IReadOnlyCollection<ContentType>>> loadById,
        Func<ContentType, KeyType> keyExtractor,
        Func<ContentType, IdType> idExtractor,
        Func<ContentType, DateTime> entryTimeExtractor,
        Func<ContentType, bool> entryIsValidForCache,
        Func<Task<IReadOnlyCollection<ContentType>>>? initialLoadProducer = null,
        Action<string, Exception?>? errorLogger = null
        )
    {
        _loadByKey = loadByKey;
        _loadById = loadById;
        _keyExtractor = keyExtractor;
        _idExtractor = idExtractor;
        _entryTimeExtractor = entryTimeExtractor;
        _initialLoadProducer = initialLoadProducer;
        _entryIsValidForCache = entryIsValidForCache;
        _errorLogger = errorLogger;

        if (_initialLoadProducer is not null)
        {
            Task.Run(PerformInitialLoad);
        }
        else if(!_initialLoadTaskCompletionSource.Task.IsCompleted)
        {
            _initialLoadTaskCompletionSource.SetResult();
        }
    }

    private async Task PerformInitialLoad() => await LockAndLoad(async() =>
    {
        try
        {
            var content = await _initialLoadProducer!();
            foreach (var item in content)
            {
                OptionallyInsert(item);
            }
        }
        catch (Exception ex)
        {
            _errorLogger?.Invoke("During initial load", ex);
        }
        finally
        {
            if(!_initialLoadTaskCompletionSource.Task.IsCompleted)
                _initialLoadTaskCompletionSource.SetResult();
        }
    });

    public async Task<IReadOnlyDictionary<KeyType, ContentType>> GetCached()
    {
        await _initialLoadTaskCompletionSource.Task;
        return _cache.ToDictionary(x => x.Key, x => x.Value.Content);
    }

    public async Task ReloadEntriesForKeys(IEnumerable<KeyType> keys)
    {
        var loaded = await _loadByKey(keys);
        await UpdateWith(loaded);
    }
    public async Task ReloadEntriesForIds(IEnumerable<IdType> ids)
    {
        var loaded = await _loadById(ids);
        await UpdateWith(loaded);
    }


    public async Task UpdateWith(IEnumerable<ContentType> content) => await LockAndLoad(() =>
    {
        foreach (var item in content)
        {
            OptionallyInsert(item);
        }
        return Task.CompletedTask;
    });
        
        
    public Task<IReadOnlyDictionary<KeyType, ContentType>> Retrieve(IEnumerable<KeyType> keys) => 
        Retrieve(keys, _keyExtractor, _loadByKey);

    public Task<IReadOnlyDictionary<IdType, ContentType>> Retrieve(IEnumerable<IdType> ids) =>
        Retrieve(ids, _idExtractor, _loadById);


    private async Task<IReadOnlyDictionary<LocalKeyType, ContentType>> Retrieve<LocalKeyType>(
        IEnumerable<LocalKeyType> keys, 
        Func<ContentType, LocalKeyType> toKey,
        Func<IEnumerable<LocalKeyType>, Task<IReadOnlyCollection<ContentType>>> loader 
            ) where LocalKeyType : notnull
    {
        await _initialLoadTaskCompletionSource.Task;
        var relevantKeys = keys.ToHashSet();

        var returnee = _cache
            .Select(_ => (Key: toKey(_.Value.Content), Content: _.Value.Content))
            .Where(_ => relevantKeys.Contains(_.Key))
            .GroupBy(_ => _.Key)
            .ToDictionary(_ => _.Key, _ => _.First().Content);

        var notInCache = keys
            .Where(_ => !returnee.ContainsKey(_))
            .ToList();
        if (notInCache.Any())
        {
            await LockAndLoad(async () =>
            {
                var loaded = await loader(notInCache);
                foreach (var load in loaded)
                {
                    var didInsert = OptionallyInsert(load);
                    if (didInsert)
                        returnee[toKey(load)] = load;
                }
            });
        }

        return returnee;
    }


    private async Task LockAndLoad(Func<Task> updateTask)
    {
        await _updateLock.WaitAsync();
        try
        {
            await updateTask();
            await CleanCacheForInvalid();
        }
        catch(Exception e)
        {
            _errorLogger?.Invoke("While locked", e);
            throw;
        }
        finally
        {
            _updateLock.Release();
        }   
    }

    private async Task CleanCacheForInvalid()
    {
        var toReload = new List<KeyType>();
        foreach(var (key, content) in _cache)
        {
            if(!_entryIsValidForCache(content.Content))
            {
                _cache.Remove(key);
                toReload.Add(key);
            }
        }
        if (toReload.Any())
        {
            var reloaded = await _loadByKey(toReload);
            foreach(var cont in reloaded.Where(_entryIsValidForCache))
            {
                _cache[_keyExtractor(cont)] = new(cont, DateTime.Now);
            }
        }
    }


    private bool OptionallyInsert(ContentType content)
    {
        var key = _keyExtractor(content);
        if (_cache.ContainsKey(key))
        {
            var inCache = _cache[key];
            var inCacheId = _idExtractor(inCache.Content);
            var newId = _idExtractor(content);
            if(inCacheId.Equals(newId))
            {
                _cache[key] = new(content, DateTime.Now);
                return true;
            }
            else if(_entryIsValidForCache(content))
            {
                var timeInCache = _entryTimeExtractor(inCache.Content);
                var timeNew = _entryTimeExtractor(content);
                if(timeInCache < timeNew)
                {
                    _cache[key] = new(content, DateTime.Now);
                    return true;

                }
            }
        }
        else
        {
            _cache[key] = new(content, DateTime.Now);
            return true;
        }
        return false;
    }

    private record CacheEntry<Typ>(
        Typ Content,
        DateTime CacheTime
        );


}
