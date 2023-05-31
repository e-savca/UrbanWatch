using Newtonsoft.Json;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public interface ITranzyService
    {
        public Vehicle[] GetVehiclesData();
        public Models.Route[] GetRoutesData();
        public Trip[] GetTripsData();
        public Shape[] GetShapesData();
        public Stop[] GetStopsData();
        public StopTimes[] GetStopTimesData();
    }
}
