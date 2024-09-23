using System.Runtime.CompilerServices;

namespace ThousandAcreWoods.Language.Streamables;
public class CombinedStreamContainer<T> : IStreamable<T>
{
    private readonly SemaphoreSlim _returnLock = new SemaphoreSlim(1,1);
    private readonly CancellationToken _stopToken = new CancellationToken();
    private TaskCompletionSource<IReadOnlyCollection<T>> _taskSource = new TaskCompletionSource<IReadOnlyCollection<T>>();

    public CombinedStreamContainer(params IStreamable<T>[] containers)
    {
        StartConsumption(containers);
    }




    public async IAsyncEnumerable<IReadOnlyCollection<T>> Consume([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested && !_stopToken.IsCancellationRequested)
        {
            var toReturn = await _taskSource.Task;
            yield return toReturn;
        }
    }

    private void StartConsumption(IEnumerable<IStreamable<T>> containers) 
    { 
        foreach(var container in containers) 
        {
            _ = Task.Run(async () =>
            {
                while (!_stopToken.IsCancellationRequested)
                {
                    await foreach(var message in container.Consume(_stopToken))
                    {
                        await UpdateResult(message);
                    }

                }
            });
        
        }
    }

    private async Task UpdateResult(IReadOnlyCollection<T> values)
    {
        await _returnLock.WaitAsync(_stopToken);
        try
        {
            var toComplete = _taskSource;
            _taskSource = new TaskCompletionSource<IReadOnlyCollection<T>>();
            toComplete.SetResult(values);
        }
        finally
        {
            _returnLock.Release();
        }

    }








}
