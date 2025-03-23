namespace UrbanWatch.Worker;

public class VehicleWorker : BackgroundService
{
    private readonly string _apiKey;
    private readonly ILogger<VehicleWorker> _logger;

    public VehicleWorker(
        IConfiguration config,
        ILogger<VehicleWorker> logger)
    {
        _apiKey = config["TRANZY_API_KEY_DEV01"];
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Vehicle worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("API Key: {apiKey}", _apiKey);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
