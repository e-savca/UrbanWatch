using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class DatabaseIntegrationService : IDataIntegrationService
    {
        private DataContext _dataContext;
        ApplicationContext _dbContext;
        private readonly IDataProviderService _dataProviderService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseIntegrationService> _logger;
        private int _executionCount;

        public DatabaseIntegrationService(DataContext dataContext, IDataProviderService dataProviderService, ApplicationContext dbContext, ILogger<DatabaseIntegrationService> logger)
        {
            _dataContext = dataContext;
            _dataProviderService = dataProviderService;
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task UpdateData()
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation($"DataIntegrationService is working. Count: {count}");

            using (var scope = _serviceProvider.CreateScope())
            {
                ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                try
                {
                    Vehicle[]? vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();

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
            _logger.LogInformation("DataIntegrationService is initialized.");

            var vehicles = await _dataProviderService.GetVehiclesDataAsync();
            var routes = await _dataProviderService.GetRoutesDataAsync();
            var trips = await _dataProviderService.GetTripsDataAsync();
            var shapes = await _dataProviderService.GetShapesDataAsync();
            var stops = await _dataProviderService.GetStopsDataAsync();
            var stopTimes = await _dataProviderService.GetStopTimesDataAsync();

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
            List<string?> oldDataStrings = new List<string?>();
            foreach (Vehicle item in oldData)
            {
                oldDataStrings.Add(item.ToString());
            }
            List<string?> newDataStrings = new List<string?>();
            foreach (Vehicle item in NewData)
            {
                newDataStrings.Add(item.ToString());
            }
            return oldDataStrings.SequenceEqual(newDataStrings);
        }
    }
}
