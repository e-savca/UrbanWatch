using UrbanWatch.Worker.Clients;
using UrbanWatch.Worker.Models;
using UrbanWatch.Worker.Services;

namespace UrbanWatch.Worker;

public class VehicleWorker : BackgroundService
{
    private const string AgencyId = "4";
    private readonly TranzyClient _client;
    public VehicleHistoryService VehicleHistoryService { get; }
    private readonly ILogger<VehicleWorker> _logger;

    public VehicleWorker(
        TranzyClient client,
        VehicleHistoryService vehicleHistoryService,
        ILogger<VehicleWorker> logger)
    {
        VehicleHistoryService = vehicleHistoryService;

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
            
            _logger.LogInformation("Fetching data on API {time}", DateTimeOffset.Now);
            var vehicles = await _client.GetVehiclesAsync(AgencyId);
            _logger.LogInformation("Converting data to list {time}", DateTimeOffset.Now);
            var vehicleList = new List<Vehicle>(vehicles);
            _logger.LogInformation("Save data batch async on MongoDB {time}", DateTimeOffset.Now);
            await VehicleHistoryService.SaveBatchAsync(vehicleList, stoppingToken);
            _logger.LogInformation("Check last entries {time}", DateTimeOffset.Now);
            var lastEntries = await VehicleHistoryService.GetLastAsync();
            _logger.LogInformation($"Last entries count {lastEntries.Count}");
            foreach (var vehicle in lastEntries)
            {
                _logger.LogInformation("Entry: {vehicle} {time}",vehicle.ToString(), DateTimeOffset.Now);
            }
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}
