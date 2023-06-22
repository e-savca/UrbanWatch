using UrbanWatchMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.Services.Interfaces;

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
            return await _dataContext.Trips.AsQueryable().ToListAsync();
        }
        public async Task<Trip> GetTheTripAsync(string? tripId)
        {
            Trip? trip = await _dataContext.Trips.AsQueryable().FirstOrDefaultAsync(trip => trip.TripId == tripId);
            if (trip == null)
            {
                throw new Exception("Trip not found");
            }
            return trip;
        }
        public async Task<List<Models.Route>> GetRoutesAsync()
        {
            return await _dataContext.Routes.AsQueryable().OrderBy(item => item.RouteShortName).ToListAsync();
        }
        public async Task<Models.Route> GetTheRouteAsync(string? routeId)
        {
            Models.Route? route = await _dataContext.Routes.AsQueryable().FirstOrDefaultAsync(route => route.RouteId == routeId);
            if (route == null)
            {
                throw new Exception("Route not found");
            }
            return route;
        }
        public async Task<List<Shape>> GetShapesAsync(string? shapeId)
        {
            var shapes = await _dataContext.Shapes.Where(shape => shape.ShapeId == shapeId).AsQueryable().ToListAsync();
            if (shapes == null)
            {
                throw new Exception("Shapes not found");
            }
            return shapes;
        }
        public async Task<List<Stop>> GetStopsAsync(string? shapeId)
        {
            List<Stop> stopsList = new List<Stop>();
            var stopTimes = await _dataContext.StopTimes.Where(stoptime => stoptime.TripId == shapeId).AsQueryable().ToListAsync();
            var stops = await _dataContext.Stops.AsQueryable().ToListAsync();
            foreach (StopTimes stopTime in stopTimes)
            {
                stopsList.Add(stops.FirstOrDefault(stop => stop.StopId == stopTime.StopId));
            }
            return stopsList;
        }
        public async Task<List<StopTimes>> GetStopTimesAsync()
        {            
            return await _dataContext.StopTimes.AsQueryable().ToListAsync();
        }
        public async Task<List<Vehicle>> GetVehiclesAsync(string tripId)
        {
            DateTime dateTime = DateTime.Now.AddHours(-3).AddMinutes(-3);            
            List<Vehicle> vehiclesByTripId = await _dataContext.Vehicles.Where(vehicle => vehicle.TripId == tripId && dateTime <= vehicle.Timestamp).AsQueryable().ToListAsync();
            
            return vehiclesByTripId;
        }
    }
}
