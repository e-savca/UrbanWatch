using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class DataIntegrationService : BackgroundService
    {
        private DataContext _dataContext;
        private readonly ITranzyService _tranzyService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DataIntegrationService> _logger;
        private int _executionCount;

        public DataIntegrationService(DataContext dataContext, ITranzyService tranzyService, IServiceProvider serviceProvider, ILogger<DataIntegrationService> logger)
        {
            _dataContext = dataContext;
            _tranzyService = tranzyService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DataIntegrationService running.");

            await InitializeDatabase();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await UpdateDatabase();
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation("DataIntegrationService is stopping.");
                _logger.LogInformation($"OperationCanceledException ex.Message:\n{ex.Message}.");
            }
        }

        public async Task UpdateDatabase()
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation($"DataIntegrationService is working. Count: {count}");

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
        public async Task InitializeDatabase()
        {
            _logger.LogInformation("DataIntegrationService is initialized.");

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

            using (var scope = _serviceProvider.CreateScope())
            {
                ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                try
                {
                    //_ = await _context.Database.EnsureDeletedAsync();
                    _ = await _context.Database.EnsureCreatedAsync();                    

                    // Remove Yesterday's data from Vehicles
                    DateTime dateTime = DateTime.Today.AddMinutes(-10);
                    Vehicle[] vehiclesToRemove = await _context.Vehicles.Where(vehicle => vehicle.Timestamp <= dateTime).ToArrayAsync();
                    _context.Vehicles.RemoveRange(vehiclesToRemove);
                    await _context.Vehicles.AddRangeAsync(vehicles);                    

                    _context.Routes.RemoveRange(_context.Routes);
                    await _context.Routes.AddRangeAsync(routes);                    

                    _context.Trips.RemoveRange(_context.Trips);
                    await _context.Trips.AddRangeAsync(trips);

                    _context.Shapes.RemoveRange(_context.Shapes);
                    await _context.Shapes.AddRangeAsync(shapes);

                    _context.Stops.RemoveRange(_context.Stops);
                    await _context.Stops.AddRangeAsync(stops);                    

                    _context.StopTimes.RemoveRange(_context.StopTimes);
                    await _context.StopTimes.AddRangeAsync(stopTimes);                    

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    throw;
                }
            }

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
