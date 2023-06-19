using Newtonsoft.Json;
using System.Net.Http;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyDataProviderService : IDataProviderService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // links to Tranzy APIs
        private readonly Uri _vehiclesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/vehicles");
        private readonly Uri _routesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/routes");
        private readonly Uri _tripsAPILink = new Uri("https://api.tranzy.dev/v1/opendata/trips");
        private readonly Uri _shapesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/shapes");
        private readonly Uri _stopsAPILink = new Uri("https://api.tranzy.dev/v1/opendata/stops");
        private readonly Uri _stopTimesAPILink = new Uri("https://api.tranzy.dev/v1/opendata/stop_times");

        private readonly string _AgencyId = "4";
        private readonly string _APIKey = "vPb5jKPyRf1AS3AWrqYRL5hUHjxViy2u202HJ3h3";

        public TranzyDataProviderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private async Task<T> GetDataAsync<T>(Uri requestUri)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Agency-Id", _AgencyId);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-API-KEY", _APIKey);

            using (var response = await client.GetAsync(requestUri))
            {
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                T data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
        }
        public async Task<Vehicle[]> GetVehiclesDataAsync()
        {
            return await GetDataAsync<Vehicle[]>(_vehiclesAPILink);
        }
        public async Task<Models.Route[]> GetRoutesDataAsync()
        {
            return await GetDataAsync<Models.Route[]>(_routesAPILink);
        }
        public async Task<Trip[]> GetTripsDataAsync()
        {
            return await GetDataAsync<Trip[]>(_tripsAPILink);
        }
        public async Task<Shape[]> GetShapesDataAsync()
        {
            return await GetDataAsync<Shape[]>(_shapesAPILink);
        }
        public async Task<Stop[]> GetStopsDataAsync()
        {
            return await GetDataAsync<Stop[]>(_stopsAPILink);
        }
        public async Task<StopTimes[]> GetStopTimesDataAsync()
        {
            return await GetDataAsync<StopTimes[]>(_stopTimesAPILink);
        }
    }
}
