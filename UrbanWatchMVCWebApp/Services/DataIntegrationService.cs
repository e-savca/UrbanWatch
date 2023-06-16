using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class DataIntegrationService : BackgroundService
    {        
        private readonly ITranzyService _tranzyService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DataIntegrationService> _logger;
        private int _executionCount;

        public DataIntegrationService(ITranzyService tranzyService, IServiceProvider serviceProvider, ILogger<DataIntegrationService> logger)
        {
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
                    Vehicle[]? vehiclesFromDb = await _context.Vehicles.ToArrayAsync();
                    foreach (Vehicle vehicle in vehiclesFromService)
                    {
                        bool result = await IsDuplicateVehicle(vehicle, vehiclesFromDb);
                        if (!result)
                        {
                            _context.Vehicles.Add(vehicle);
                        }
                    }          
                    await _context.SaveChangesAsync();

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

            using (var scope = _serviceProvider.CreateScope())
            {
                ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                try
                {
                    //_ = await _context.Database.EnsureDeletedAsync();
                    _ = await _context.Database.EnsureCreatedAsync();                    

                    DateTime dateTime = DateTime.Today.AddMinutes(-10);
                    Vehicle[] vehiclesToRemove = await _context.Vehicles.Where(vehicle => vehicle.Timestamp <= dateTime).ToArrayAsync();
                    _context.Vehicles.RemoveRange(vehiclesToRemove);
                    _context.Vehicles.AddRange(vehicles);

                    _context.Routes.RemoveRange(_context.Routes);
                    _context.Routes.UpdateRange(routes);

                    _context.Trips.RemoveRange(_context.Trips);
                    _context.Trips.UpdateRange(trips);

                    _context.Shapes.RemoveRange(_context.Shapes);
                    _context.Shapes.UpdateRange(shapes);

                    _context.Stops.RemoveRange(_context.Stops);
                    _context.Stops.UpdateRange(stops);

                    _context.StopTimes.RemoveRange(_context.StopTimes);
                    _context.StopTimes.UpdateRange(stopTimes);

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
        public async Task<bool> IsDuplicateVehicle(Vehicle newVehicle, Vehicle[]? vehiclesFromDb)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                var lastVehicle = vehiclesFromDb.Where(v => v.VehicleId == newVehicle.VehicleId).OrderByDescending(v => v.Timestamp).FirstOrDefault();

                if (lastVehicle != null && lastVehicle.Timestamp >= newVehicle.Timestamp)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
