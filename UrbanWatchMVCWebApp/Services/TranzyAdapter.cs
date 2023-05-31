using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyAdapter
    {
        private ITranzyService _tranzyService;
        private string? _tripId;
        private Trip? _trip;
        private Models.Route? _route;
        public TranzyAdapter(ITranzyService tranzyService)
        {
            _tranzyService = tranzyService;
        }
        public DataContext GetDataContext(string tripId)
        {
            _tripId = tripId;
            return new DataContext()
            {
                theTrip = GetTheTrip(),
                theRoute = GetTheRoute(),
                Shapes = GetShapes(),
                Stops = GetStops(),
                Vehicles = GetVehicles()
            };
        }
        public List<List<string[]>> GetRoutes()
        {
            Shape[] shapes = _tranzyService.GetShapesData();
            List<string> shapeIds = new List<string>();
            foreach (Shape shape in shapes)
            {
                if (!shapeIds.Contains(shape.Id))
                {
                    shapeIds.Add(shape.Id);
                }
            }
            List<List<string[]>> GrouppedShapes = new List<List<string[]>>();            

            foreach (string ShapeId in shapeIds)
            {
                List<string[]> shapesById = new List<string[]>();
                foreach (Shape shape in shapes.Where(shape => shape.Id == ShapeId).ToList<Shape>())
                {
                    string[] point = new string[2];
                    point[0] = shape.Latitude;
                    point[1] = shape.Longitude;
                    shapesById.Add(point);
                }
                GrouppedShapes.Add(shapesById);
            }
            return GrouppedShapes;
        }
        private Trip GetTheTrip()
        {
            _trip = _tranzyService.GetTripsData().FirstOrDefault(trip => trip.tripId == _tripId);
            return _trip;
        }
        private Models.Route GetTheRoute()
        {
            _route = _tranzyService.GetRoutesData().FirstOrDefault(route => route.Id == _trip.routeId);
            return _route;
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
