using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public interface ITranzyAdapter
    {
        public Task<Trip[]> GetTripsAsync();
        public Task<Trip> GetTheTripAsync(string tripId);
        public Task<Models.Route[]> GetRoutesAsync();
        public Task<Models.Route> GetTheRouteAsync(int? routeId);
        public Task<Shape[]> GetShapesAsync(string? shapeId);
        public Task<Stop[]> GetStopsAsync(string? shapeId);
        public Task<Vehicle[]> GetVehiclesAsync(string tripId);
    }

}
