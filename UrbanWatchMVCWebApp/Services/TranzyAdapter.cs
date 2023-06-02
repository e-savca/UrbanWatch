using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyAdapter
    {
        private ITranzyService _tranzyService;
        private Trip? _trip;
        public TranzyAdapter(ITranzyService tranzyService)
        {
            _tranzyService = tranzyService;
        }
        public DataContext GetDataContext(string tripId)
        {
            _trip = GetTheTrip(tripId);
            return new DataContext()
            {
                theTrip = _trip,
                theRoute = GetTheRoute(),
                Shapes = GetShapes(),
                Stops = GetStops(),
                Vehicles = GetVehicles()
            };
        }
        private Models.Route[] GetRoutes()
        {
            return _tranzyService.GetRoutesData();
        }
        private Trip GetTheTrip(string tripId)
        {
            return _tranzyService.GetTripsData().FirstOrDefault(trip => trip.tripId == tripId);
        }
        private Models.Route GetTheRoute()
        {
            return GetRoutes().FirstOrDefault(route => route.Id == _trip.routeId);
        }
        private Shape[] GetShapes()
        {
            return _tranzyService.GetShapesData().Where(shape => shape.Id == _trip.shapeId).ToArray();
        }
        private Stop[] GetStops()
        {
            List<Stop> stops = new List<Stop>();
            foreach (StopTimes stopTime in _tranzyService.GetStopTimesData().Where(stoptime => stoptime.tripId == _trip.shapeId).ToArray())
            {
                stops.Add(_tranzyService.GetStopsData().FirstOrDefault(stop => stop.Id == stopTime.stopId));
            }

            return stops.ToArray();
        }
        private Vehicle[] GetVehicles()
        {
            return _tranzyService.GetVehiclesData().Where(vehicle => vehicle.tripId == _trip.tripId).ToArray();
        }


    }
}
