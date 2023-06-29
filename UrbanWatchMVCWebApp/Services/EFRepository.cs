using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.IServices;

namespace UrbanWatchMVCWebApp.Services
{
    public class EFRepository : IRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly MappingService _mappingService;
        public EFRepository(ApplicationContext dbContext, MappingService mappingService)
        {
            _dbContext = dbContext;
            _mappingService = mappingService;
        }
        public async Task<List<Models.UiModels.Trip>> GetTripsAsync()
        {
            var trips =  _dbContext.Trips.AsQueryable();
            return await _mappingService
                .DoMapping<Models.UiModels.Trip>(trips)
                .ToListAsync();
        }
        public async Task<Models.UiModels.Trip?> GetTheTripAsync(string? theRouteId, string tripTypeString)
        {
            var trip = await _dbContext.Trips
                .SingleOrDefaultAsync(trip => trip.RouteId == theRouteId && trip.DirectionId == tripTypeString);
            return _mappingService
                    .DoMapping<Models.UiModels.Trip>(trip);
        }
        public async Task<List<Models.UiModels.Route>> GetRoutesAsync()
        {
            var routes =  _dbContext.Routes.OrderBy(item => item.RouteShortName).AsQueryable();
            return await _mappingService
                .DoMapping<Models.UiModels.Route>(routes)
                .ToListAsync();
        }
        public async Task<Models.UiModels.Route?> GetTheRouteAsync(string? routeId)
        {
            var route = await _dbContext.Routes.SingleOrDefaultAsync(item => item.RouteId == routeId);
            return _mappingService
                .DoMapping<Models.UiModels.Route>(route);
        }
        public async Task<List<Models.UiModels.Shape>> GetShapesAsync(string? shapeId)
        {
            var shapes = _dbContext.Shapes.Where(item => item.ShapeId == shapeId).AsQueryable();
            return await _mappingService
                .DoMapping<Models.UiModels.Shape>(shapes)
                .ToListAsync();
        }
        public async Task<List<Models.UiModels.Stop>> GetStopsAsync(string? shapeId)
        {
            var stopTimes = _dbContext.StopTimes.Where(stoptime => stoptime.TripId == shapeId).AsQueryable();
            var stops = _dbContext.Stops.AsQueryable();
            var stopsList = stopTimes
                .Select(stopTime => stops.FirstOrDefault(stop => stop.StopId == stopTime.StopId))
                .AsQueryable();
            return await _mappingService
                .DoMapping<Models.UiModels.Stop>(stopsList)
                .ToListAsync();
        }
        public async Task<List<Models.UiModels.StopTimes>> GetStopTimesAsync()
        {
            var stopTimes = _dbContext.StopTimes.AsQueryable();
            return await _mappingService
                .DoMapping<Models.UiModels.StopTimes>(stopTimes)
                .ToListAsync();
        }
        public async Task<List<Models.UiModels.Vehicle>> GetVehiclesAsync(string tripId)
        {
            DateTimeOffset utcTime = DateTimeOffset.UtcNow.AddMinutes(-3);
            var vehiclesByTripId = _dbContext.Vehicles
                .Where(vehicle => vehicle.TripId == tripId && utcTime <= vehicle.Timestamp)
                .AsQueryable();
            return await _mappingService
                .DoMapping<Models.UiModels.Vehicle>(vehiclesByTripId)
                .ToListAsync();
        }
    }
}
