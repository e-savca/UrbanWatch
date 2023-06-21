using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class DatabaseIntegrationService : IDataIntegrationService
    {
        private readonly DataContext _dataContext;
        private readonly ApplicationContext _dbContext;
        private readonly IDataProviderService _dataProviderService;
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

            using (var scope = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Vehicle[]? vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();

                    _logger.LogInformation($"Call IsDuplicateVehicle. Count: {count} {DateTime.Now}");
                    if (!_dataContext.AreVehiclesDuplicates(vehiclesFromService))
                    {
                        _dataContext.Vehicles = vehiclesFromService;
                        await _dbContext.Vehicles.AddRangeAsync(vehiclesFromService);
                        await _dbContext.SaveChangesAsync();
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

            _dataContext.Vehicles = await _dataProviderService.GetVehiclesDataAsync();
            _dataContext.Routes = await _dataProviderService.GetRoutesDataAsync();
            _dataContext.Trips = await _dataProviderService.GetTripsDataAsync();
            _dataContext.Shapes = await _dataProviderService.GetShapesDataAsync();
            _dataContext.Stops = await _dataProviderService.GetStopsDataAsync();
            _dataContext.StopTimes = await _dataProviderService.GetStopTimesDataAsync();

            using (var scope = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _ = await _dbContext.Database.EnsureDeletedAsync();
                    _ = await _dbContext.Database.EnsureCreatedAsync();                    

                    // Remove Yesterday's data from Vehicles
                    DateTime dateTime = DateTime.Today.AddMinutes(-10);
                    Vehicle[] vehiclesToRemove = await _dbContext.Vehicles.Where(vehicle => vehicle.Timestamp <= dateTime).ToArrayAsync();
                    _dbContext.Vehicles.RemoveRange(vehiclesToRemove);
                    await _dbContext.Vehicles.AddRangeAsync(_dataContext.Vehicles);                    

                    _dbContext.Routes.RemoveRange(_dbContext.Routes);
                    await _dbContext.Routes.AddRangeAsync(_dataContext.Routes);                    

                    _dbContext.Trips.RemoveRange(_dbContext.Trips);
                    await _dbContext.Trips.AddRangeAsync(_dataContext.Trips);

                    _dbContext.Shapes.RemoveRange(_dbContext.Shapes);
                    await _dbContext.Shapes.AddRangeAsync(_dataContext.Shapes);

                    _dbContext.Stops.RemoveRange(_dbContext.Stops);
                    await _dbContext.Stops.AddRangeAsync(_dataContext.Stops);

                    _dbContext.StopTimes.RemoveRange(_dbContext.StopTimes);
                    await _dbContext.StopTimes.AddRangeAsync(_dataContext.StopTimes);                    

                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    throw;
                }
            }
        }        
    }
}
