using System.Runtime.CompilerServices;

namespace ThousandAcreWoods.Language.Streamables;

public class LoadingStreamable<T> : ILoadingStreamable<T>
{
    private readonly Func<Task<IEnumerable<T>>> _loader;
    private readonly SemaphoreSlim _triggerLock = new SemaphoreSlim(1, 1);
    private readonly TimeSpan? _delayTime;
    private TaskCompletionSource<IReadOnlyCollection<T>> _currentCompletionSource = new TaskCompletionSource<IReadOnlyCollection<T>>();
    private Task? _currentUpdateTask;



    public LoadingStreamable(Func<Task<IEnumerable<T>>> loader, int delayTimeInMilliseconds = 0)
    {
        _loader = loader;
        if(delayTimeInMilliseconds > 0)
            _delayTime = TimeSpan.FromMilliseconds(delayTimeInMilliseconds);
    }

    public async IAsyncEnumerable<IReadOnlyCollection<T>> Consume([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var nextResult = await _currentCompletionSource.Task;
            yield return nextResult;
        }
    }

    public async Task ReloadAndPublish()
    {
        try
        {
            await _triggerLock.WaitAsync();
            if (_currentUpdateTask == null)
                _currentUpdateTask = Task.Run(ExecuteUpdate);
        }
        finally
        {
            _triggerLock.Release();
        }
    }


    private async Task ExecuteUpdate()
    {
        if(_delayTime.HasValue)
            await Task.Delay(_delayTime.Value);
        var loaded = await _loader();
        var nextResult = loaded.ToList();
        var toComplete = _currentCompletionSource;
        _currentCompletionSource = new TaskCompletionSource<IReadOnlyCollection<T>>();
        toComplete.SetResult(nextResult);
        try
        {
            await _triggerLock.WaitAsync();
            _currentUpdateTask = null;
        }
        finally { _triggerLock.Release(); }
    }
}
