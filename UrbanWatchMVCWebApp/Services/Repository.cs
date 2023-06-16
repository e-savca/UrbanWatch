using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace UrbanWatchMVCWebApp.Services
{
    public class Repository : IRepository
    {
        private readonly DataContext _dataContext;
        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Trip[]> GetTripsAsync()
        {
            return await Task.FromResult(_dataContext.Trips);
        }
        public async Task<Trip> GetTheTripAsync(string? tripId)
        {
            Trip? trip = await Task.FromResult(_dataContext.Trips.FirstOrDefault(trip => trip.TripId == tripId));
            if (trip == null)
            {
                throw new Exception("Trip not found");
            }
            return trip;
        }
        public async Task<Models.Route[]> GetRoutesAsync()
        {
            return await Task.FromResult(_dataContext.Routes.OrderBy(item => item.RouteShortName).ToArray());
        }
        public async Task<Models.Route> GetTheRouteAsync(string? routeId)
        {
            Models.Route? route = await Task.FromResult(_dataContext.Routes.FirstOrDefault(route => route.RouteId == routeId));
            if (route == null)
            {
                throw new Exception("Route not found");
            }
            return route;
        }
        public async Task<Shape[]> GetShapesAsync(string? shapeId)
        {
            Shape[]? shapes = await Task.FromResult(_dataContext.Shapes.Where(shape => shape.ShapeId == shapeId).ToArray());
            if (shapes == null)
            {
                throw new Exception("Shapes not found");
            }
            return shapes;
        }
        public async Task<Stop[]> GetStopsAsync(string? shapeId)
        {
            List<Stop> stopsList = new List<Stop>();
            StopTimes[] stopTimes = await Task.FromResult(_dataContext.StopTimes.Where(stoptime => stoptime.TripId == shapeId).ToArray());
            Stop[] stops = await Task.FromResult(_dataContext.Stops.ToArray());
            foreach (StopTimes stopTime in stopTimes)
            {
                stopsList.Add(stops.SingleOrDefault(stop => stop.StopId == stopTime.StopId));
            }
            return stopsList.ToArray();
        }
        public async Task<Vehicle[]> GetVehiclesAsync(string tripId)
        {
            DateTime dateTime = DateTime.Now.AddHours(-3).AddMinutes(-3);            
            List<Vehicle> vehiclesByTripId = await Task.FromResult(_dataContext.Vehicles.Where(vehicle => vehicle.TripId == tripId && dateTime <= vehicle.Timestamp).ToList());
            
            return vehiclesByTripId.ToArray();
        }
    }
}
