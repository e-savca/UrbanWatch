using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.Services.Interfaces;
using UrbanWatchMVCWebApp.Models.UiModels;

namespace UrbanWatchMVCWebApp.Services
{
    public class Repository : IRepository
    {
        private readonly DataContext _dataContext;
        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Trip>> GetTripsAsync()
        {
            return await _dataContext.Trips.ToListAsync();
        }
        public async Task<Trip> GetTheTripAsync(string? tripId)
        {
            Trip? trip = await _dataContext.Trips.FirstOrDefaultAsync(trip => trip.TripId == tripId);
            if (trip == null)
            {
                throw new Exception("Trip not found");
            }
            return trip;
        }
        public async Task<List<Models.UiModels.Route>> GetRoutesAsync()
        {
            return await _dataContext.Routes.OrderBy(item => item.RouteShortName).ToListAsync();
        }
        public async Task<Models.UiModels.Route> GetTheRouteAsync(string? routeId)
        {
            Models.UiModels.Route? route = await _dataContext.Routes.FirstOrDefaultAsync(route => route.RouteId == routeId);
            if (route == null)
            {
                throw new Exception("Route not found");
            }
            return route;
        }
        public async Task<List<Shape>> GetShapesAsync(string? shapeId)
        {
            var shapes = await _dataContext.Shapes.Where(shape => shape.ShapeId == shapeId).ToListAsync();
            if (shapes == null)
            {
                throw new Exception("Shapes not found");
            }
            return shapes;
        }
        public async Task<List<Stop>> GetStopsAsync(string? shapeId)
        {
            var stopTimes =  _dataContext.StopTimes.Where(stoptime => stoptime.TripId == shapeId);//.ToListAsync();
            var stops = _dataContext.Stops;//.ToListAsync();

            List<Stop?> stopsList = await stopTimes.Select(stopTime => stops.FirstOrDefault(stop => stop.StopId == stopTime.StopId))
                .ToListAsync();

            return stopsList.Where(r => r is not null).ToList();
        }
        public async Task<List<StopTimes>> GetStopTimesAsync()
        {            
            return await _dataContext.StopTimes.ToListAsync();
        }
        public async Task<List<Vehicle>> GetVehiclesAsync(string tripId)
        {
            DateTime dateTime = DateTime.Now.AddHours(-3).AddMinutes(-3);            
            // todo: review to use datetimeoffset
            List<Vehicle> vehiclesByTripId = await _dataContext.Vehicles
                .Where(vehicle => vehicle.TripId == tripId && dateTime <= vehicle.Timestamp).ToListAsync();
            
            return vehiclesByTripId;
        }
    }
}
