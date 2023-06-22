using UrbanWatchMVCWebApp.Services.Interfaces;

namespace UrbanWatchMVCWebApp.Services;
public class MemoryIntegrationService : IDataIntegrationService
{
    private DataContext _dataContext;
    private readonly IDataProviderService _dataProviderService;
    private readonly MappingService _mappingService;
    private readonly ILogger<MemoryIntegrationService> _logger;
    private int _executionCount;

    public MemoryIntegrationService(DataContext dataContext, IDataProviderService dataProviderService, MappingService mappingService, ILogger<MemoryIntegrationService> logger)
    {
        _dataContext = dataContext;
        _dataProviderService = dataProviderService;
        _mappingService = mappingService;
        _logger = logger;
    }
    public async Task UpdateData()
    {
        var count = Interlocked.Increment(ref _executionCount);
        _logger.LogInformation($"MemoryIntegrationService is working. Count: {count}");

        try
        {
            var vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();
            var vehiclesFromServiceMapped = _mappingService.DoMapping<List<Models.ApiModels.TranzyV1Models.Vehicle>, List<Models.Vehicle>>(vehiclesFromService);


            _logger.LogInformation($"Call IsDuplicateVehicle. Count: {count} {DateTime.Now}");
            if (!await _dataContext.AreVehiclesDuplicatesAsync(vehiclesFromServiceMapped))
            {                    
                _dataContext.Vehicles = vehiclesFromServiceMapped;
            }
        }
        catch (Exception)
        {
             throw;
        }

    }
    public async Task InitializeData()
    {
        _logger.LogInformation("MemoryIntegrationService is initialized.");
        var vehicleAsAPIModel = await _dataProviderService.GetVehiclesDataAsync();
        _dataContext.Vehicles = _mappingService.DoMapping<List<Models.ApiModels.TranzyV1Models.Vehicle>, List<Models.Vehicle>>(vehicleAsAPIModel);

        var routesAsAPIModel = await _dataProviderService.GetRoutesDataAsync();
        _dataContext.Routes = _mappingService.DoMapping<List<Models.ApiModels.TranzyV1Models.Route>, List<Models.Route>>(routesAsAPIModel);

        var tripsAsAPIModel = await _dataProviderService.GetTripsDataAsync();
        _dataContext.Trips = _mappingService.DoMapping<List<Models.ApiModels.TranzyV1Models.Trip>, List<Models.Trip>>(tripsAsAPIModel);

        var shapesAsAPIModel = await _dataProviderService.GetShapesDataAsync();
        _dataContext.Shapes = _mappingService.DoMapping<List<Models.ApiModels.TranzyV1Models.Shape>, List<Models.Shape>>(shapesAsAPIModel);


        var stopsAsAPIModel = await _dataProviderService.GetStopsDataAsync();
        _dataContext.Stops = _mappingService.DoMapping<List<Models.ApiModels.TranzyV1Models.Stop>, List<Models.Stop>>(stopsAsAPIModel);

        var stopTimesAsAPIModel = await _dataProviderService.GetStopTimesDataAsync();
        _dataContext.StopTimes = _mappingService.DoMapping<List<Models.ApiModels.TranzyV1Models.StopTimes>, List<Models.StopTimes>>(stopTimesAsAPIModel);
    }
}
