using System.Security.Cryptography;
using System.Text;
using UrbanWatch.Worker.Clients;
using UrbanWatch.Worker.ConfigManager;
using UrbanWatch.Worker.Infrastructure.Data;
using UrbanWatch.Worker.Models;
using UrbanWatch.Worker.Services;

namespace UrbanWatch.Worker;

public class VehicleWorker : BackgroundService
{
    private const string AgencyId = "4";
    private readonly TranzyClient _client;
    public VehicleHistoryService VehicleHistoryService { get; }
    // public RedisContext RedisContext { get; }
    public EnvManager EnvManager { get; }
    private readonly ILogger<VehicleWorker> _logger;

    private readonly Dictionary<string, string> _vehiclesHashCache = new Dictionary<string, string>();

    public VehicleWorker(
        TranzyClient client,
        VehicleHistoryService vehicleHistoryService,
        // RedisContext redisContext,
        EnvManager envManager,
        ILogger<VehicleWorker> logger)
    {
        VehicleHistoryService = vehicleHistoryService;
        // RedisContext = redisContext;
        EnvManager = envManager;

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

            var vehicles = await _client.GetVehiclesAsync(AgencyId);
            
            if (_vehiclesHashCache.Count != 0)
            {
                var rnd = new Random();
                var sample = vehicles.OrderBy(_ => rnd.Next()).Take(20).ToList();
            
                bool anyChanged = sample.Any(s =>
                {
                    var currentHash = ComputeVehicleHash(s);
                    if (s.VehicleId == null) return false;
                    return !_vehiclesHashCache.TryGetValue(s.VehicleId, out var hash) || hash != currentHash;
                });
                if (anyChanged)
                {
                    await VehicleHistoryService.SaveBatchAsync(vehicles, stoppingToken);
                    AddOrUpdateVehiclesHash(vehicles);
                }
            }
            else
            {
                AddOrUpdateVehiclesHash(vehicles);
                await VehicleHistoryService.SaveBatchAsync(vehicles, stoppingToken);
            }
            
            if (!EnvManager.IsDevelopment())
                await Task.Delay(5000, stoppingToken);
            else
                await Task.Delay(10000, stoppingToken);
        }
    }

    private void AddOrUpdateVehiclesHash(List<Vehicle> vehicles)
    {
        foreach (var v in vehicles)
        {
            if (!string.IsNullOrWhiteSpace(v.VehicleId))
                _vehiclesHashCache[v.VehicleId] = ComputeVehicleHash(v);
        }
    }

    private string ComputeVehicleHash(Vehicle vehicle)
    {
        var input = $"{vehicle.Latitude}_{vehicle.Longitude}_{vehicle.Timestamp}";
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}