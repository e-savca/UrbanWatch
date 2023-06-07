using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyAdapter : ITranzyAdapter
    {
        private ITranzyService _tranzyServiceLocal = new TranzyServiceLocal();
        private ITranzyService _tranzyServiceWebAPI = new TranzyServiceWebAPI();
        public async Task<Trip[]> GetTripsAsync()
        {
            return await _tranzyServiceLocal.GetTripsDataAsync();
        }        
        public async Task<Trip> GetTheTripAsync(string tripId)
        {            
            var body = await GetTripsAsync();
            return body.FirstOrDefault(trip => trip.tripId == tripId);

        }
        public async Task<Models.Route[]> GetRoutesAsync()
        {
            var body = await _tranzyServiceLocal.GetRoutesDataAsync();
            return body.OrderBy(item => item.routeShortName).ToArray();
        }
        public async Task<Models.Route> GetTheRouteAsync(int? routeId)
        {
            var body = await GetRoutesAsync();
            return body.FirstOrDefault(route => route.Id == routeId);
        }
        public async Task<Shape[]> GetShapesAsync(string? shapeId)
        {
            var body = await _tranzyServiceLocal.GetShapesDataAsync();
            return body.Where(shape => shape.Id == shapeId).ToArray();
        }
        public async Task<Stop[]> GetStopsAsync(string? shapeId)
        {
            List<Stop> stops = new List<Stop>();
            StopTimes[] stopTimes = await _tranzyServiceLocal.GetStopTimesDataAsync();
            Stop[] stopsData = await _tranzyServiceLocal.GetStopsDataAsync();
            foreach (StopTimes stopTime in stopTimes.Where(stoptime => stoptime.tripId == shapeId).ToArray())
            {
                stops.Add(stopsData.FirstOrDefault(stop => stop.Id == stopTime.stopId));
            }
            return stops.ToArray();
        }
        public async Task<Vehicle[]> GetVehiclesAsync(string tripId)
        {
            Vehicle[] body = await _tranzyServiceWebAPI.GetVehiclesDataAsync();
            return body.Where(vehicle => vehicle.tripId == tripId).ToArray();
        }
    }
}
