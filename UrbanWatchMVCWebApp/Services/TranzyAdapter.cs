using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyAdapter : ITranzyAdapter
    {
        private ITranzyService _tranzyServiceLocal = new TranzyServiceLocal();
        private ITranzyService _tranzyServiceWebAPI = new TranzyServiceWebAPI();
        public Trip[] GetTrips()
        {
            return _tranzyServiceLocal.GetTripsData();
        }        
        public Trip GetTheTrip(string tripId)
        {
            return GetTrips().FirstOrDefault(trip => trip.tripId == tripId);
        }
        public Models.Route[] GetRoutes()
        {
            return _tranzyServiceLocal.GetRoutesData().OrderBy(item => item.routeShortName).ToArray();
        }
        public Models.Route GetTheRoute(int? routeId)
        {
            return GetRoutes().FirstOrDefault(route => route.Id == routeId);
        }
        public Shape[] GetShapes(string? shapeId)
        {
            return _tranzyServiceLocal.GetShapesData().Where(shape => shape.Id == shapeId).ToArray();
        }
        public Stop[] GetStops(string? shapeId)
        {
            List<Stop> stops = new List<Stop>();
            foreach (StopTimes stopTime in _tranzyServiceLocal.GetStopTimesData().Where(stoptime => stoptime.tripId == shapeId).ToArray())
            {
                stops.Add(_tranzyServiceLocal.GetStopsData().FirstOrDefault(stop => stop.Id == stopTime.stopId));
            }

            return stops.ToArray();
        }
        public Vehicle[] GetVehicles(string tripId)
        {
            return _tranzyServiceWebAPI.GetVehiclesData().Where(vehicle => vehicle.tripId == tripId).ToArray();
        }
    }
}
