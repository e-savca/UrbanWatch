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

            if (!_dbContext.Database.CanConnect())
            {
                throw new Exception("Database connection error.");
            }
        }
        public async Task UpdateData()
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation($"IDataProviderService is working. Count: {count}");

            var vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();
            var vehiclesFromServiceMappedToUi = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehiclesFromService).AsQueryable();

            _dataContext.Vehicles = vehiclesFromServiceMappedToUi;


            await using var scope = await _dbContext.Database.BeginTransactionAsync();
            try
            {

                _logger.LogInformation($"Call IsDuplicateVehicle. Count: {count} {DateTime.Now}");
                if (!await _dataContext.AreVehiclesDuplicatesAsync(vehiclesFromServiceMappedToUi))
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
        public async Task InitializeData()
        {

            _logger.LogInformation("IDataProviderService is initialized.");

            // Mapping data and add to DataContext
            var vehicleAsApiModel = await _dataProviderService.GetVehiclesDataAsync();
            var vehicleAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Vehicle>>(vehicleAsApiModel).AsQueryable();
            _dataContext.Vehicles = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehicleAsApiModel).AsQueryable();

            var routesAsApiModel = await _dataProviderService.GetRoutesDataAsync();
            var routesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Route>>(routesAsApiModel).AsQueryable();
            _dataContext.Routes = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Route>>(routesAsApiModel).AsQueryable();

            var tripsAsApiModel = await _dataProviderService.GetTripsDataAsync();
            var tripsAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Trip>>(tripsAsApiModel).AsQueryable();
            _dataContext.Trips = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Trip>>(tripsAsApiModel).AsQueryable();

            var shapesAsApiModel = await _dataProviderService.GetShapesDataAsync();
            var shapesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Shape>>(shapesAsApiModel).AsQueryable();
            _dataContext.Shapes = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Shape>>(shapesAsApiModel).AsQueryable();

            var stopsAsApiModel = await _dataProviderService.GetStopsDataAsync();
            var stopsAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.Stop>>(stopsAsApiModel).AsQueryable();
            _dataContext.Stops = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.Stop>>(stopsAsApiModel).AsQueryable();

            var stopTimesAsApiModel = await _dataProviderService.GetStopTimesDataAsync();
            var stopTimesAsDataModel = _mappingService
                .DoMapping<IEnumerable<Models.DataModels.StopTimes>>(stopTimesAsApiModel).AsQueryable();
            _dataContext.StopTimes = _mappingService
                .DoMapping<IEnumerable<Models.UiModels.StopTimes>>(stopTimesAsApiModel).AsQueryable();

            // Add data to EF
            await using var scope = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //_ = await _dbContext.Database.EnsureDeletedAsync();
                _ = await _dbContext.Database.EnsureCreatedAsync();

                _dbContext.RemoveRange(_dbContext.Vehicles);
                _dbContext.RemoveRange(_dbContext.Routes);
                _dbContext.RemoveRange(_dbContext.Trips);
                _dbContext.RemoveRange(_dbContext.Shapes);
                _dbContext.RemoveRange(_dbContext.Stops);
                _dbContext.RemoveRange(_dbContext.StopTimes);

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
