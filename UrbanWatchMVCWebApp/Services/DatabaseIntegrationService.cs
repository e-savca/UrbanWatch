using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.IServices;
using UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;
using Vehicle = UrbanWatchMVCWebApp.Models.UiModels.Vehicle;

namespace UrbanWatchMVCWebApp.Services
{
    public class DatabaseIntegrationService : IDataIntegrationService
    {
        private readonly RoutesDataSnapshot _routesDataSnapshot;
        private readonly ApplicationContext _dbContext;
        private readonly IDataProviderService _dataProviderService;
        private readonly MappingService _mappingService;
        private readonly ILogger<DatabaseIntegrationService> _logger;
        private int _executionCount;

        public DatabaseIntegrationService(
            RoutesDataSnapshot routesDataSnapshot,
            IDataProviderService dataProviderService,
            ApplicationContext dbContext,
            MappingService mappingService,
            ILogger<DatabaseIntegrationService> logger)
        {
            _routesDataSnapshot = routesDataSnapshot;
            _dataProviderService = dataProviderService;
            _dbContext = dbContext;
            _mappingService = mappingService;
            _logger = logger;

            if (!_dbContext.Database.CanConnect())
            {
                throw new Exception("Database connection error.");
            }
        }
        public async Task UpdateDataAsync()
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation($"IDataProviderService is working. Count: {count}");

            var vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();
            var vehiclesFromServiceMappedToUi = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehiclesFromService).AsQueryable();

            _routesDataSnapshot.Vehicles = vehiclesFromServiceMappedToUi;


            await using var scope = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {

                _logger.LogInformation($"Call IsDuplicateVehicle. Count: {count} {DateTime.Now}");
                if (!await _routesDataSnapshot.AreVehiclesDuplicatesAsync(vehiclesFromServiceMappedToUi))
                {
                    _logger.LogInformation($"Add data to EF. Count: {count} {DateTime.Now}");

                    var vehiclesFromServiceMappedToEf = _mappingService
                        .DoMapping<IEnumerable<Models.DataModels.Vehicle>>(vehiclesFromService).AsQueryable();

                    await _dbContext.Vehicles.AddRangeAsync(vehiclesFromServiceMappedToEf);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private SemaphoreSlim r = new SemaphoreSlim(1, 1);
        private bool init = false;

        public async Task InitializeDataAsync()
        {
            if (init) return;
            await r.WaitAsync();

            if (init) return;
            _logger.LogInformation("IDataProviderService is initialized.");

            // Mapping data and add to DataContext
            var vehicleAsApiModel = await _dataProviderService.GetVehiclesDataAsync();
            var vehicleAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Vehicle>>(vehicleAsApiModel).AsQueryable();
            _routesDataSnapshot.Vehicles = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehicleAsApiModel).AsQueryable();

            var routesAsApiModel = await _dataProviderService.GetRoutesDataAsync();
            var routesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Route>>(routesAsApiModel).AsQueryable();

            var tripsAsApiModel = await _dataProviderService.GetTripsDataAsync();
            var tripsAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Trip>>(tripsAsApiModel).AsQueryable();

            var shapesAsApiModel = await _dataProviderService.GetShapesDataAsync();
            var shapesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Shape>>(shapesAsApiModel).AsQueryable();

            var stopsAsApiModel = await _dataProviderService.GetStopsDataAsync();
            var stopsAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Stop>>(stopsAsApiModel).AsQueryable();

            var stopTimesAsApiModel = await _dataProviderService.GetStopTimesDataAsync();
            var stopTimesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.StopTimes>>(stopTimesAsApiModel).AsQueryable();

            // Add data to EF
            try
            {
                _ = await _dbContext.Database.EnsureDeletedAsync();
                _ = await _dbContext.Database.EnsureCreatedAsync();


                await _dbContext.Vehicles.AddRangeAsync(vehicleAsDataModel);
                await _dbContext.Routes.AddRangeAsync(routesAsDataModel);
                await _dbContext.Trips.AddRangeAsync(tripsAsDataModel);
                await _dbContext.Shapes.AddRangeAsync(shapesAsDataModel);
                await _dbContext.Stops.AddRangeAsync(stopsAsDataModel);
                await _dbContext.StopTimes.AddRangeAsync(stopTimesAsDataModel);

                await _dbContext.SaveChangesAsync();
                init = true;
                r.Release();
            }
            catch (Exception ex)
            {
                r.Release();
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
    }
}
