using UrbanWatch.Worker.Clients;

namespace UrbanWatch.Worker;

public class VehicleWorker : BackgroundService
{
    private readonly TranzyClient _client;
    private readonly ILogger<VehicleWorker> _logger;

    public VehicleWorker(
        TranzyClient client,
        ILogger<VehicleWorker> logger)
    {

        _client = client;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Vehicle worker running at: {time}", DateTimeOffset.Now);
            }

            var vehicles = await _client.GetVehiclesAsync("04");
            
            _logger.LogInformation("Vehicle count: {count}", vehicles.Length);
            
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}
