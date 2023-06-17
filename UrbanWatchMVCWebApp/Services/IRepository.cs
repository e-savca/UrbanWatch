using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public interface IRepository
    {
        public Task<Trip[]> GetTripsAsync();
        public Task<Trip> GetTheTripAsync(string tripId);
        public Task<Models.Route[]> GetRoutesAsync();
        public Task<Models.Route> GetTheRouteAsync(string? routeId);
        public Task<Shape[]> GetShapesAsync(string? shapeId);
        public Task<Stop[]> GetStopsAsync(string? shapeId);
        public Task<StopTimes[]> GetStopTimesAsync();
        public Task<Vehicle[]> GetVehiclesAsync(string tripId);
    }

}
