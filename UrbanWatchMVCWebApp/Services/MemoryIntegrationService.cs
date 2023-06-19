using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class MemoryIntegrationService : BackgroundService, IDataIntegrationService
    {
        private DataContext _dataContext;
        private readonly ITranzyService _tranzyService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MemoryIntegrationService> _logger;
        private int _executionCount;

        public MemoryIntegrationService(DataContext dataContext, ITranzyService tranzyService, IServiceProvider serviceProvider, ILogger<MemoryIntegrationService> logger)
        {
            _dataContext = dataContext;
            _tranzyService = tranzyService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MemoryIntegrationService running.");

            await InitializeData();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await UpdateData();
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation("MemoryIntegrationService is stopping.");
                _logger.LogInformation($"OperationCanceledException ex.Message:\n{ex.Message}.");
            }
        }

        public async Task UpdateData()
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation($"MemoryIntegrationService is working. Count: {count}");

            using (var scope = _serviceProvider.CreateScope())
            {
                ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                try
                {
                    Vehicle[]? vehiclesFromService = await _tranzyService.GetVehiclesDataAsync();

                    if (!IsDuplicateVehicle(_dataContext.Vehicles, vehiclesFromService))
                    {
                        _dataContext.Vehicles = vehiclesFromService;
                        await _context.Vehicles.AddRangeAsync(vehiclesFromService);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        public async Task InitializeData()
        {
            _logger.LogInformation("MemoryIntegrationService is initialized.");

            var vehicles = await _tranzyService.GetVehiclesDataAsync();
            var routes = await _tranzyService.GetRoutesDataAsync();
            var trips = await _tranzyService.GetTripsDataAsync();
            var shapes = await _tranzyService.GetShapesDataAsync();
            var stops = await _tranzyService.GetStopsDataAsync();
            var stopTimes = await _tranzyService.GetStopTimesDataAsync();

            _dataContext.Vehicles = vehicles;
            _dataContext.Routes = routes;
            _dataContext.Trips = trips;
            _dataContext.Shapes = shapes;
            _dataContext.Stops = stops;
            _dataContext.StopTimes = stopTimes;
        }
        public bool IsDuplicateVehicle(Vehicle[] oldData, Vehicle[] NewData)
        {
            List<string> oldDataStrings = new List<string>();
            foreach (Vehicle item in oldData)
            {
                oldDataStrings.Add(item.Timestamp.ToString());
            }
            List<string> newDataStrings = new List<string>();
            foreach (Vehicle item in NewData)
            {
                newDataStrings.Add(item.Timestamp.ToString());
            }
            return oldDataStrings.SequenceEqual(newDataStrings);
        }
    }
}
