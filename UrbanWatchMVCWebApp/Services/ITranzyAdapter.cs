using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public interface ITranzyAdapter
    {
        public Trip[] GetTrips();
        public Trip GetTheTrip(string tripId);
        public Models.Route[] GetRoutes();
        public Models.Route GetTheRoute(int? routeId);
        public Shape[] GetShapes(string? shapeId);
        public Stop[] GetStops(string? shapeId);
        public Vehicle[] GetVehicles(string tripId);
    }

}
