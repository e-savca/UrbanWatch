using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.Models.UiModels;
using UrbanWatchMVCWebApp.IServices;

namespace UrbanWatchMVCWebApp.Services
{
    public class DataContextRepository : IRepository
    {
        private readonly FullDataSnapshot _fullDataSnapshot;
        public DataContextRepository(FullDataSnapshot fullDataSnapshot)
        {
            _fullDataSnapshot = fullDataSnapshot;
        }
        public async Task<List<Trip>> GetTripsAsync()
        {
            return await _fullDataSnapshot.Trips.ToListAsync();
        }
        public async Task<Trip?> GetTheTripAsync(string? theRouteId, string tripTypeString)
        {
            return await _fullDataSnapshot.Trips.FirstOrDefaultAsync(trip => trip.RouteId == theRouteId && trip.DirectionId == tripTypeString);
        }
        public async Task<List<Models.UiModels.Route>> GetRoutesAsync()
        {
            return await _fullDataSnapshot.Routes.OrderBy(item => item.RouteShortName).ToListAsync();
        }
        public async Task<Models.UiModels.Route?> GetTheRouteAsync(string? routeId)
        {
            var route = await _fullDataSnapshot.Routes.FirstOrDefaultAsync(route => route.RouteId == routeId);
            if (route == null)
            {
                throw new Exception("Route not found");
            }
            return route;
        }
        public async Task<List<Shape>> GetShapesAsync(string? shapeId)
        {
            var shapes = await _fullDataSnapshot.Shapes.Where(shape => shape.ShapeId == shapeId).ToListAsync();
            if (shapes == null)
            {
                throw new Exception("Shapes not found");
            }
            return shapes;
        }
        public async Task<List<Stop>> GetStopsAsync(string? shapeId)
        {
            var stopTimes = await _fullDataSnapshot.StopTimes.Where(stoptime => stoptime.TripId == shapeId).ToListAsync();
            var stops = await _fullDataSnapshot.Stops.ToListAsync();

            var stopsList = await stopTimes.Select(stopTime => stops.FirstOrDefault(stop => stop.StopId == stopTime.StopId))
                .AsQueryable().ToListAsync();

            return stopsList.Where(r => r is not null).ToList();
        }
        public async Task<List<StopTimes>> GetStopTimesAsync()
        {
            return await _fullDataSnapshot.StopTimes.ToListAsync();
        }
        public async Task<List<Vehicle>> GetVehiclesAsync(string tripId)
        {
            var utcTime = DateTimeOffset.UtcNow.AddMinutes(-3);
            var vehiclesByTripId = await _fullDataSnapshot.Vehicles
                .Where(vehicle => vehicle.TripId == tripId && utcTime <= vehicle.Timestamp).ToListAsync();

            return vehiclesByTripId;
        }
    }
}
