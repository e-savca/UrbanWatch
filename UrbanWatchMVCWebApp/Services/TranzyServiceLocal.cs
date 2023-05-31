using Newtonsoft.Json;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyServiceLocal : ITranzyService
    {
        private readonly string _vehiclesFile = "vehicles.txt";
        private readonly string _routesFile = "routes.txt";
        private readonly string _tripsFile = "trips.txt";
        private readonly string _shapesFile = "shapes.txt";
        private readonly string _stopsFile = "stops.txt";
        private readonly string _stopTimesFile = "stop_times.txt";
        private string GetData(string file)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + file;
            return File.ReadAllText(filePath);
        }
        public Vehicle[] GetVehiclesData()
        {
            var body = GetData(_vehiclesFile);
            return JsonConvert.DeserializeObject<Vehicle[]>(body);
        }
        public Models.Route[] GetRoutesData()
        {
            var body = GetData(_routesFile);
            return JsonConvert.DeserializeObject<Models.Route[]>(body);
        }
        public Trip[] GetTripsData()
        {
            var body = GetData(_tripsFile);
            return JsonConvert.DeserializeObject<Trip[]>(body);
        }
        public Shape[] GetShapesData()
        {
            var body = GetData(_shapesFile);
            return JsonConvert.DeserializeObject<Shape[]>(body);
        }
        public Stop[] GetStopsData()
        {
            var body = GetData(_stopsFile);
            return JsonConvert.DeserializeObject<Stop[]>(body);
        }
        public StopTimes[] GetStopTimesData()
        {
            var body = GetData(_stopTimesFile);
            return JsonConvert.DeserializeObject<StopTimes[]>(body);
        }
    }
}
