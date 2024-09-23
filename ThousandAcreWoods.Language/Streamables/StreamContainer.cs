using System.Runtime.CompilerServices;

namespace ThousandAcreWoods.Language.Streamables;

public class StreamContainer<T> : IStreamContainer<T>
{

    private readonly bool _keepUnread;
    private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
    private TaskCompletionSource<IReadOnlyCollection<T>> _source = new TaskCompletionSource<IReadOnlyCollection<T>>();
    private bool _currentHasBeenRead = false;
    private List<T> _latestResult = new List<T>();

    public StreamContainer(bool keepUnread = false)
    {
        _keepUnread = keepUnread;
    }

    public async IAsyncEnumerable<IReadOnlyCollection<T>> Consume([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            var toReturn = await _source.Task;
            yield return toReturn;
            if (!_currentHasBeenRead)
                await SetRead();
        }
    }

    public void Set(IEnumerable<T> values) => UpdateResult(values);
  
    public void SetSingle(T value) => UpdateResult(new[] {value});

    private void UpdateResult(IEnumerable<T> values)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await _lock.WaitAsync();
                if (!_currentHasBeenRead && _keepUnread)
                {
                    values = _latestResult.Concat(values);
                }
                else _latestResult.Clear();
                var toComplete = _source;
                _source = new TaskCompletionSource<IReadOnlyCollection<T>>();
                toComplete.SetResult(values.ToList());
                _currentHasBeenRead = false;
                _latestResult.AddRange(values);
            }
            finally
            {
                _ = _lock.Release();
            }
        });
    }

    private async Task SetRead()
    {
        await _lock.WaitAsync();
        _currentHasBeenRead = true;
        _lock.Release();
    }


}
