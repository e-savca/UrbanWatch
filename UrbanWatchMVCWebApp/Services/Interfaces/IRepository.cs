using UrbanWatchMVCWebApp.Models.UiModels;

namespace UrbanWatchMVCWebApp.Services.Interfaces
{
    public interface IRepository
    {
        public Task<List<Trip>> GetTripsAsync();
        public Task<Trip> GetTheTripAsync(string tripId);
        public Task<List<Models.UiModels.Route>> GetRoutesAsync();
        public Task<Models.UiModels.Route> GetTheRouteAsync(string? routeId);
        public Task<List<Shape>> GetShapesAsync(string? shapeId);
        public Task<List<Stop>> GetStopsAsync(string? shapeId);
        public Task<List<StopTimes>> GetStopTimesAsync();
        public Task<List<Vehicle>> GetVehiclesAsync(string tripId);
    }

}
