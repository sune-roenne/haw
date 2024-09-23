using static ThousandAcreWoods.Language.IAsyncProcessingQueue;

namespace ThousandAcreWoods.Language;

public interface IAsyncProcessingQueue
{
    internal enum ProcessingMode
    {
        OneAtATime = 1,
        AllAvailable = 2
    }

    /// <summary>
    /// Creates an async queue that will process the queue one element at a time
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="consumerAction"></param>
    /// <param name="numberOfConcurrentConsumers"></param>
    /// <param name="stopToken"></param>
    /// <returns></returns>

    public static IAsyncProcessingQueue<T> CreateSingleProcessingQueue<T>(Func<T, Task> consumerAction, int numberOfConcurrentConsumers = 1, CancellationToken? stopToken = null) => CreateSingleProcessingQueue<T, bool>(
        consumerAction: item =>
        {
            consumerAction(item);
            return Task.FromResult(false);
        },
        numberOfConcurrentConsumers: numberOfConcurrentConsumers,
        stopToken: stopToken);


    /// <summary>
    /// Creates an async queue that will pull all available items from queue for processing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="consumerAction"></param>
    /// <param name="numberOfConcurrentConsumers"></param>
    /// <param name="stopToken"></param>
    /// <returns></returns>

    public static IAsyncProcessingQueue<T> CreateAllProcessingQueue<T>(Func<IEnumerable<T>, Task> consumerAction, int numberOfConcurrentConsumers = 1, CancellationToken? stopToken = null) => CreateAllProcessingQueue<T, bool>(
        consumerAction: items =>
        {
            consumerAction(items);
            var returnee = items.Select(_ => false);
            return Task.FromResult(returnee);
        },
        numberOfConcurrentConsumers: numberOfConcurrentConsumers,
        stopToken: stopToken);


    /// <summary>
    /// Creates an async queue that will process the queue one element at a time and return the result of applying <paramref name="consumerAction"/>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="consumerAction"></param>
    /// <param name="numberOfConcurrentConsumers"></param>
    /// <param name="stopToken"></param>
    /// <returns></returns>

    public static IAsyncProcessingQueue<TIn, TOut> CreateSingleProcessingQueue<TIn, TOut>(Func<TIn, Task<TOut>> consumerAction, int numberOfConcurrentConsumers = 1, CancellationToken? stopToken = null) => new AsyncProcessingQueue<TIn, TOut>(
        numberOfConsumers: numberOfConcurrentConsumers,
        processor: async (IEnumerable<TIn> items) => await Task.WhenAll(items.Select(item => consumerAction(item))),
        processingMode: ProcessingMode.OneAtATime,
        stopToken: stopToken);

    /// <summary>
    /// Creates an async queue that will pull all available items from queue for processing and return the results of applying <paramref name="consumerAction"/>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="consumerAction"></param>
    /// <param name="numberOfConcurrentConsumers"></param>
    /// <param name="stopToken"></param>
    /// <returns></returns>

    public static IAsyncProcessingQueue<TIn, TOut> CreateAllProcessingQueue<TIn, TOut>(Func<IEnumerable<TIn>, Task<IEnumerable<TOut>>> consumerAction, int numberOfConcurrentConsumers = 1, CancellationToken? stopToken = null) => new AsyncProcessingQueue<TIn, TOut>(
        numberOfConsumers: numberOfConcurrentConsumers,
        processor: consumerAction,
        processingMode: ProcessingMode.AllAvailable,
        stopToken: stopToken);


}


public interface IAsyncProcessingQueue<T>
{
    Task Enqueue(T item);
    Task EnqueueAll(IEnumerable<T> items);
}

public interface IAsyncProcessingQueue<TIn, TOut> : IAsyncProcessingQueue<TIn>
{
    Task<TOut> Process(TIn item);
    Task<IEnumerable<TOut>> ProcessAll(IEnumerable<TIn> items);

    Task IAsyncProcessingQueue<TIn>.Enqueue(TIn item) => Process(item);
    Task IAsyncProcessingQueue<TIn>.EnqueueAll(IEnumerable<TIn> items) => ProcessAll(items);

}


public class AsyncProcessingQueue<TIn, TOut> : IAsyncProcessingQueue<TIn, TOut>
{
    private readonly CancellationToken _stopToken;
    private readonly QueueConsumer[] _consumers;



    internal AsyncProcessingQueue(
        int numberOfConsumers, 
        Func<IEnumerable<TIn>, Task<IEnumerable<TOut>>> processor, 
        ProcessingMode processingMode,
        CancellationToken? stopToken)
    {
        _stopToken = stopToken ?? new CancellationToken();
        _consumers = Enumerable.Range(0, numberOfConsumers)
            .Select(_ => new QueueConsumer(processor, processingMode, _stopToken))
            .ToArray();

    }

    public Task<TOut> Process(TIn item)
    {
        var consumer = _consumers.MinBy(_ => _.ProcessLoad)!;
        return consumer.Process(item);
    }

    public Task<IEnumerable<TOut>> ProcessAll(IEnumerable<TIn> items)
    {
        var consumer = _consumers.MinBy(_ => _.ProcessLoad)!;
        return consumer.ProcessAll(items);
    }


    private class QueueConsumer
    {
        public (int CountBeingProcessed, int CountOnQueue) ProcessLoad => (_currentlyProcessed.Count, _backingQueue.Count);
        private readonly List<QueueRecord> _currentlyProcessed = new List<QueueRecord>();
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1,1);
        private readonly CancellationToken _stopToken;
        private readonly Func<IEnumerable<TIn>, Task<IEnumerable<TOut>>> _consumerFunction;
        private readonly Queue<QueueRecord> _backingQueue = new Queue<QueueRecord>();
        private ProcessingMode _processingMode;
        private TaskCompletionSource _inputWaitTask = new TaskCompletionSource();

        public QueueConsumer(Func<IEnumerable<TIn>, Task<IEnumerable<TOut>>> consumerFunction, ProcessingMode processingMode, CancellationToken stopToken)
        {
            _stopToken = stopToken;
            _consumerFunction = consumerFunction;
            _processingMode = processingMode;
            StartConsuming();
        }


        public Task<TOut> Process(TIn item) => ThrowAnotherShrimpOnTheBarbie(new SingleItemQueueRecord(item), _ => _.CompletionSource.Task );

        public Task<IEnumerable<TOut>> ProcessAll(IEnumerable<TIn> items) => ThrowAnotherShrimpOnTheBarbie(new MultiItemQueueRecord(items),_ => _.CompletionSource.Task);

        private async Task<TProcOut> ThrowAnotherShrimpOnTheBarbie<TProcOut, TRec>(TRec queueRecord, Func<TRec, Task<TProcOut>> resultExtractor) where TRec : QueueRecord
        {
            await ExecuteLocked(() =>
            {
                _backingQueue.Enqueue(queueRecord);
                _inputWaitTask.SetResult();
                _inputWaitTask = new TaskCompletionSource();
            });
            var returnee = await resultExtractor(queueRecord);
            return returnee;
        }


        private void StartConsuming()
        {
            _ = Task.Run(async () => {

                while (!_stopToken.IsCancellationRequested)
                {
                    while (!_backingQueue.Any())
                        await _inputWaitTask.Task.WaitAsync(_stopToken);
                    if (_processingMode == ProcessingMode.OneAtATime)
                        await ConsumeOneAtATime();
                    else if(_processingMode == ProcessingMode.AllAvailable)
                        await ConsumeAll();
                }

            });
        }


        private async Task ConsumeOneAtATime()
        {
            while (!_stopToken.IsCancellationRequested && _backingQueue.Any())
            {
                QueueRecord? next = default;
                await ExecuteLocked(() =>
                {
                    next = _backingQueue.Dequeue();
                });
                if(next != null) 
                {
                    await ExecuteFor(next);
                }
            }
        }

        private async Task ConsumeAll()
        {
            QueueRecord[] items = [];
            await ExecuteLocked(() =>
            {
                items = _backingQueue.ToArray();
                _backingQueue.Clear();
            });
            if (items.Any())
            {
                await ExecuteFor(items);
            }
        }

        private async Task ExecuteFor(params QueueRecord[] records)
        {
            var inputAndRecords = records
                .SelectMany(rec => rec.Items.Select(_ => (Record: rec, Input: _)))
                .ToList();
            var inputs = inputAndRecords
                .Select(_ => _.Input)
                .ToList();

            try
            {
                await ExecuteLocked(() => { _currentlyProcessed.AddRange(records); });
                var results = (await _consumerFunction(inputs))
                    .ToList();
                var resultMappings = inputAndRecords.Zip(results)
                    .GroupBy(_ => _.First.Record.Id)
                    .Select(_ => (Record: _.First().First.Record, Results: _.Select(_ => _.Second).ToList()))
                    .ToList();

                foreach(var (record, outputs) in resultMappings)
                {
                    if(record is SingleItemQueueRecord sing)
                    {
                        sing.CompletionSource.SetResult(outputs.First());
                    }
                    if(record is MultiItemQueueRecord mult)
                    {
                        mult.CompletionSource.SetResult(outputs);
                    }
                }
            } catch(Exception e)
            {
                foreach(var rec in records)
                    rec.SetException(e);
            }
            await ExecuteLocked(() => { _currentlyProcessed.Clear(); });
        }

        private async Task ExecuteLocked(Action act)
        {
            await _lock.WaitAsync();
            try
            {
                act();
            }
            finally
            {
                _lock.Release();
            }

        }

        private abstract record QueueRecord()
        {
            private static long _currentId = 0L;
            private static object _idLock = new { };
            private static long NextId()
            {
                lock (_idLock)
                {
                    return ++_currentId;
                }

            }
            public long Id = NextId(); 
            public abstract IEnumerable<TIn> Items { get; }
            public abstract void SetException(Exception e);

        }

        private record SingleItemQueueRecord(TIn Item) : QueueRecord()
        {
            public override IEnumerable<TIn> Items => [Item];
            public TaskCompletionSource<TOut> CompletionSource = new TaskCompletionSource<TOut>();
            public override void SetException(Exception e)
            {
                if(!CompletionSource.Task.IsCompleted)
                    CompletionSource.SetException(e);
            }
        }

        private record MultiItemQueueRecord(IEnumerable<TIn> MultiItems) : QueueRecord()
        {
            public override IEnumerable<TIn> Items => MultiItems;
            public TaskCompletionSource<IEnumerable<TOut>> CompletionSource = new TaskCompletionSource<IEnumerable<TOut>>();
            public override void SetException(Exception e)
            {
                if (!CompletionSource.Task.IsCompleted)
                    CompletionSource.SetException(e);
            }

        }


    }

}
