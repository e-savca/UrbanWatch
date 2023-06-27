using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Services.Interfaces;

namespace UrbanWatchMVCWebApp.Services
{
    public class DatabaseIntegrationService : IDataIntegrationService
    {
        private readonly DataContext _dataContext;
        private readonly ApplicationContext _dbContext;
        private readonly IDataProviderService _dataProviderService;
        private readonly MappingService _mappingService;
        private readonly ILogger<DatabaseIntegrationService> _logger;
        private int _executionCount;

        public DatabaseIntegrationService(DataContext dataContext, IDataProviderService dataProviderService,
            ApplicationContext dbContext, MappingService mappingService,
            ILogger<DatabaseIntegrationService> logger)
        {
            _dataContext = dataContext;
            _dataProviderService = dataProviderService;
            _dbContext = dbContext;
            _mappingService = mappingService;
            _logger = logger;
        }
        public async Task UpdateData()
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation($"IDataProviderService is working. Count: {count}");

            var vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();
            var vehiclesFromServiceMappedToUI = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehiclesFromService).AsQueryable();

            _dataContext.Vehicles = vehiclesFromServiceMappedToUI;


            using (var scope = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    _logger.LogInformation($"Call IsDuplicateVehicle. Count: {count} {DateTime.Now}");
                    if (!await _dataContext.AreVehiclesDuplicatesAsync(vehiclesFromServiceMappedToUI))
                    {
                        _logger.LogInformation($"Add data to EF. Count: {count} {DateTime.Now}");

                        var vehiclesFromServiceMappedToEF = _mappingService
                            .DoMapping<IEnumerable<Models.DataModels.Vehicle>>(vehiclesFromService).AsQueryable();

                        await _dbContext.Vehicles.AddRangeAsync(vehiclesFromServiceMappedToEF);
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
            if (_dataContext.StopTimes is not null && _dataContext.StopTimes.Any())
            {
                return;
            }

            _logger.LogInformation("IDataProviderService is initialized.");

            // Mapping data and add to DataContext
            var vehicleAsAPIModel = await _dataProviderService.GetVehiclesDataAsync();
            var vehicleAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Vehicle>>(vehicleAsAPIModel).AsQueryable();
            _dataContext.Vehicles = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehicleAsAPIModel).AsQueryable();

            var routesAsAPIModel = await _dataProviderService.GetRoutesDataAsync();
            var routesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Route>>(routesAsAPIModel).AsQueryable();
            _dataContext.Routes = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Route>>(routesAsAPIModel).AsQueryable();

            var tripsAsAPIModel = await _dataProviderService.GetTripsDataAsync();
            var tripsAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Trip>>(tripsAsAPIModel).AsQueryable();
            _dataContext.Trips = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Trip>>(tripsAsAPIModel).AsQueryable();

            var shapesAsAPIModel = await _dataProviderService.GetShapesDataAsync();
            var shapesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Shape>>(shapesAsAPIModel).AsQueryable();
            _dataContext.Shapes = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Shape>>(shapesAsAPIModel).AsQueryable();

            var stopsAsAPIModel = await _dataProviderService.GetStopsDataAsync();
            var stopsAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Stop>>(stopsAsAPIModel).AsQueryable();
            _dataContext.Stops = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Stop>>(stopsAsAPIModel).AsQueryable();

            var stopTimesAsAPIModel = await _dataProviderService.GetStopTimesDataAsync();
            var stopTimesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.StopTimes>>(stopTimesAsAPIModel).AsQueryable();
            _dataContext.StopTimes = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.StopTimes>>(stopTimesAsAPIModel).AsQueryable();

            // Add data to EF
            using (var scope = _dbContext.Database.BeginTransaction())
            {
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
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.StackTrace);
                    throw;
                }
            }
        }
    }
}
