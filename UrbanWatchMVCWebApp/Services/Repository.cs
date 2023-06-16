using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace UrbanWatchMVCWebApp.Services
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext _context;
        public Repository(ApplicationContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Trip[]> GetTripsAsync()
        {
            return await _context.Trips.ToArrayAsync();
        }
        public async Task<Trip> GetTheTripAsync(string? tripId)
        {
            Trip? trip = await _context.Trips.SingleOrDefaultAsync(trip => trip.TripId == tripId);
            if (trip == null)
            {
                throw new Exception("Trip not found");
            }

            return trip;
        }
        public async Task<Models.Route[]> GetRoutesAsync()
        {
            return await _context.Routes.OrderBy(item => item.RouteShortName).ToArrayAsync();
        }
        public async Task<Models.Route> GetTheRouteAsync(string? routeId)
        {
            Models.Route? route = await _context.Routes.SingleOrDefaultAsync(route => route.RouteId == routeId);
            if (route == null)
            {
                throw new Exception("Route not found");
            }
            return route;
        }
        public async Task<Shape[]> GetShapesAsync(string? shapeId)
        {
            Shape[]? shapes = await _context.Shapes.Where(shape => shape.ShapeId == shapeId).ToArrayAsync();
            if (shapes == null)
            {
                throw new Exception("Shapes not found");
            }
            return shapes;
        }
        public async Task<Stop[]> GetStopsAsync(string? shapeId)
        {
            List<Stop> stopsList = new List<Stop>();
            StopTimes[] stopTimes = await _context.StopTimes.Where(stoptime => stoptime.TripId == shapeId).ToArrayAsync();
            Stop[] stops = await _context.Stops.ToArrayAsync();
            foreach (StopTimes stopTime in stopTimes)
            {
                stopsList.Add(stops.SingleOrDefault(stop => stop.StopId == stopTime.StopId));
            }
            return stopsList.ToArray();
        }
        public async Task<Vehicle[]> GetVehiclesAsync(string tripId)
        {
            DateTime dateTime = DateTime.Now.AddHours(-3).AddMinutes(-3);            
            List<Vehicle> vehiclesByTripId = await _context.Vehicles.Where(vehicle => vehicle.TripId == tripId && dateTime <= vehicle.Timestamp).ToListAsync();
            List<string> vehicleIds = new List<string>();
            List<Vehicle> vehiclesByTimeStamp = new List<Vehicle>();
            foreach (Vehicle vehicle in vehiclesByTripId)
            {
                if (!vehicleIds.Contains(vehicle.Label))
                {
                    vehicleIds.Add(vehicle.Label);
                }
            }
            foreach (string vehicleId in vehicleIds)
            {
                Vehicle vehicle = vehiclesByTripId.OrderByDescending(vehicle => vehicle.Timestamp).FirstOrDefault(vehicle => vehicle.Label == vehicleId);
                vehiclesByTimeStamp.Add(vehicle);                
            }
            return vehiclesByTimeStamp.ToArray();
        }
    }
}
