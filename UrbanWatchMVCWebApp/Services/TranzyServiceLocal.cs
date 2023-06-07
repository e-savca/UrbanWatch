using Newtonsoft.Json;
using UrbanWatchMVCWebApp.Models;
using System.Linq;

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
        private async Task<string> GetDataAsync(string file)
        {
            string filePath = @"wwwroot\TranzyData\" + file;
            return await File.ReadAllTextAsync(filePath);
        }
        public async Task<Vehicle[]> GetVehiclesDataAsync()
        {
            var body = await GetDataAsync(_vehiclesFile);
            return JsonConvert.DeserializeObject<Vehicle[]>(body);
        }
        public async Task<Models.Route[]> GetRoutesDataAsync()
        {
            var body = await GetDataAsync(_routesFile);
            return JsonConvert.DeserializeObject<Models.Route[]>(body);
        }
        public async Task<Trip[]> GetTripsDataAsync()
        {
            var body = await GetDataAsync(_tripsFile);
            return JsonConvert.DeserializeObject<Trip[]>(body);
        }
        public async Task<Shape[]> GetShapesDataAsync()
        {
            var body = await GetDataAsync(_shapesFile);
            return JsonConvert.DeserializeObject<Shape[]>(body);
        }
        public async Task<Stop[]> GetStopsDataAsync()
        {
            var body = await GetDataAsync(_stopsFile);
            return JsonConvert.DeserializeObject<Stop[]>(body);
        }
        public async Task<StopTimes[]> GetStopTimesDataAsync()
        {
            var body = await GetDataAsync(_stopTimesFile);
            return JsonConvert.DeserializeObject<StopTimes[]>(body);
        }
    }
}
