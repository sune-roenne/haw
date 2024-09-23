namespace ThousandAcreWoods.Language; 
public class TaskThrottler<T>
{
    private readonly Func<Task<T>> _producer;
    private readonly SemaphoreSlim _lock = new SemaphoreSlim(1,1);
    private Task<T>? _currentTask;
    private readonly Action<Exception>? _exceptionLogger;

    public TaskThrottler(Func<Task<T>> producer, Action<Exception>? exceptionLogger = null)
    {
        _producer = producer;
        _exceptionLogger = exceptionLogger;
    }

    public async Task<T> Produce(CancellationToken cancelToken)
    {
        Task<T>? toAwait = null;
        await Locked(() =>
        {

            if (_currentTask == null || _currentTask.IsCompleted)
                _currentTask = Task.Run(async () => {
                    var ret = await _producer();
                    await Locked(() =>
                    {
                        _currentTask = null;
                        return Task.CompletedTask;
                    });
                    return ret;
                });
            toAwait = _currentTask;
            return Task.CompletedTask;
        });
        var returnee = await toAwait!.WaitAsync(cancelToken);
        return returnee;
    }

    private async Task Locked(Func<Task> action)
    {
        await _lock.WaitAsync();
        try { await action(); }
        catch (Exception e)
        {
            _exceptionLogger?.Invoke(e);
            _currentTask = null;
            throw;
        }
        finally
        {
            _lock.Release();
        }
    }


}
