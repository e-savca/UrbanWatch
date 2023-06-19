using Newtonsoft.Json;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyServiceWebAPI : ITranzyService
    {
        // links to Tranzy APIs
        private readonly Uri _vehiclesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/vehicles");
        private readonly Uri _routesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/routes");
        private readonly Uri _tripsAPILink = new Uri("https://api.tranzy.dev/v1/opendata/trips");
        private readonly Uri _shapesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/shapes");
        private readonly Uri _stopsAPILink = new Uri("https://api.tranzy.dev/v1/opendata/stops");
        private readonly Uri _stopTimesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/stop_times");

        private readonly string _AgencyId = "4";
        private readonly string _APIKey = "vPb5jKPyRf1AS3AWrqYRL5hUHjxViy2u202HJ3h3";
        private async Task<string> GetDataAsync(Uri RequestUri)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = RequestUri,
                Headers =
                {
                    { "X-Agency-Id", _AgencyId },
                    { "Accept", "application/json" },
                    { "X-API-KEY", _APIKey },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
        public async Task<Vehicle[]> GetVehiclesDataAsync()
        {
            var body = await GetDataAsync(_vehiclesAPILink);
            return JsonConvert.DeserializeObject<Vehicle[]>(body);
        }
        public async Task<Models.Route[]> GetRoutesDataAsync()
        {
            var body = await GetDataAsync(_routesAPILink);
            return JsonConvert.DeserializeObject<Models.Route[]>(body);
        }
        public async Task<Trip[]> GetTripsDataAsync()
        {
            var body = await GetDataAsync(_tripsAPILink);
            return JsonConvert.DeserializeObject<Trip[]>(body);
        }
        public async Task<Shape[]> GetShapesDataAsync()
        {
            var body = await GetDataAsync(_shapesAPILink);
            return JsonConvert.DeserializeObject<Shape[]>(body);
        }
        public async Task<Stop[]> GetStopsDataAsync()
        {
            var body = await GetDataAsync(_stopsAPILink);
            return JsonConvert.DeserializeObject<Stop[]>(body);
        }
        public async Task<StopTimes[]> GetStopTimesDataAsync()
        {
            var body = await GetDataAsync(_stopTimesAPILink);
            return JsonConvert.DeserializeObject<StopTimes[]>(body);
        }
    }
}
