
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThousandAcreWoods.Application.Platform.Services;

namespace ThousandAcreWoods.Application.Workers;

public class RecurringNotificationWorker : BackgroundService
{

    private readonly IServiceScopeFactory _scopeFactory;

    public RecurringNotificationWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var sleepSpan = TimeSpan.FromSeconds(RecurringNotificationService.FrequencyBaseSeconds);
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IRecurringNotificationService>();
        while (!stoppingToken.IsCancellationRequested)
        {
            service.Fire();
            await Task.Delay(sleepSpan, stoppingToken);
        }
    }







}



internal class RecurringNotificationService : IRecurringNotificationService
{
    public event EventHandler OnVeryOften;
    public event EventHandler OnOften;
    public event EventHandler OnNormal;
    public event EventHandler OnInfrequent;
    public event EventHandler OnVeryInfrequent;
    public event EventHandler OnDaily;

    private DateTime _lastVeryOften = DateTime.MinValue;
    private DateTime _lastOften = DateTime.MinValue;
    private DateTime _lastNormal = DateTime.MinValue;
    private DateTime _lastInfrequent = DateTime.MinValue;
    private DateTime _lastVeryInfrequent = DateTime.MinValue;
    private DateTime _lastDaily = DateTime.MinValue;
    public const int FrequencyBaseSeconds = 5;
    private int _veryOftenSeconds = FrequencyBaseSeconds;
    private int _oftenSeconds = FrequencyBaseSeconds * 4;
    private int _normalSeconds = FrequencyBaseSeconds * 10;
    private int _infrequentSeconds = FrequencyBaseSeconds * 60;
    private int _veryInfrequentSeconds = FrequencyBaseSeconds * 1_200;
    private int _dailySeconds = 60 * 60 * 24;



    


    private void FireOpt(EventHandler? handler, int frequencyInSeconds, ref DateTime lastExecution)
    {
        try
        {
            var now = DateTime.Now;
            if (now > lastExecution.AddSeconds(frequencyInSeconds))
            {
                handler?.Invoke(this, EventArgs.Empty);
                lastExecution = now;
            }
        }
        catch (Exception) { }
    }

    public void Fire()
    {
        FireOpt(OnVeryOften, _veryOftenSeconds, ref _lastVeryOften);
        FireOpt(OnOften, _oftenSeconds, ref _lastOften);
        FireOpt(OnNormal, _normalSeconds, ref _lastNormal);
        FireOpt(OnInfrequent, _infrequentSeconds, ref _lastInfrequent);
        FireOpt(OnVeryInfrequent, _veryInfrequentSeconds, ref _lastVeryInfrequent);
        FireOpt(OnDaily, _dailySeconds, ref _lastDaily);
    }
}
