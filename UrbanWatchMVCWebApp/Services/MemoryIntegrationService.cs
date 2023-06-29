using UrbanWatchMVCWebApp.IServices;

namespace UrbanWatchMVCWebApp.Services;
public class MemoryIntegrationService : IDataIntegrationService
{
    private readonly FullDataSnapshot _fullDataSnapshot;
    private readonly IDataProviderService _dataProviderService;
    private readonly MappingService _mappingService;
    private readonly ILogger<MemoryIntegrationService> _logger;
    private int _executionCount;

    public MemoryIntegrationService(
        FullDataSnapshot fullDataSnapshot,
        IDataProviderService dataProviderService, 
        MappingService mappingService,
        ILogger<MemoryIntegrationService> logger)
    {
        _fullDataSnapshot = fullDataSnapshot;
        _dataProviderService = dataProviderService;
        _mappingService = mappingService;
        _logger = logger;
    }
    public async Task UpdateDataAsync()
    {
        var count = Interlocked.Increment(ref _executionCount);
        _logger.LogInformation($"MemoryIntegrationService is working. Count: {count}");

        try
        {
            var vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();
            var vehiclesFromServiceMapped = _mappingService
                    .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehiclesFromService).AsQueryable();


            _logger.LogInformation($"Call IsDuplicateVehicle. Count: {count} {DateTime.Now}");
            if (!await _fullDataSnapshot.AreVehiclesDuplicatesAsync(vehiclesFromServiceMapped))
            {
                _fullDataSnapshot.Vehicles = vehiclesFromServiceMapped;
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
        _logger.LogInformation("MemoryIntegrationService is initialized.");

        var vehicleAsApiModel = await _dataProviderService.GetVehiclesDataAsync();
        _fullDataSnapshot.Vehicles = _mappingService
            .DoMapping<IEnumerable<Models.UiModels.Vehicle>>(vehicleAsApiModel).AsQueryable();

        var routesAsApiModel = await _dataProviderService.GetRoutesDataAsync();
        _fullDataSnapshot.Routes = _mappingService
            .DoMapping<IEnumerable<Models.UiModels.Route>>(routesAsApiModel).AsQueryable();

        var tripsAsApiModel = await _dataProviderService.GetTripsDataAsync();
        _fullDataSnapshot.Trips = _mappingService
            .DoMapping<IEnumerable<Models.UiModels.Trip>>(tripsAsApiModel).AsQueryable();

        var shapesAsApiModel = await _dataProviderService.GetShapesDataAsync();
        _fullDataSnapshot.Shapes = _mappingService
            .DoMapping<IEnumerable<Models.UiModels.Shape>>(shapesAsApiModel).AsQueryable();

        var stopsAsApiModel = await _dataProviderService.GetStopsDataAsync();
        _fullDataSnapshot.Stops = _mappingService
            .DoMapping<IEnumerable<Models.UiModels.Stop>>(stopsAsApiModel).AsQueryable();

        var stopTimesAsApiModel = await _dataProviderService.GetStopTimesDataAsync();
        _fullDataSnapshot.StopTimes = _mappingService
            .DoMapping<IEnumerable<Models.UiModels.StopTimes>>(stopTimesAsApiModel).AsQueryable();

        init = true;
        r.Release();
    }
}
