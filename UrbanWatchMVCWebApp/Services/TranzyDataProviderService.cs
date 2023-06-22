using Newtonsoft.Json;
using System.Collections.Generic;
using UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;
using UrbanWatchMVCWebApp.Services.Interfaces;

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
        public async Task<List<Vehicle>> GetVehiclesDataAsync()
        {
            return await GetDataAsync<List<Vehicle>>(_vehiclesAPILink);
        }
        public async Task<List<Models.ApiModels.TranzyV1Models.Route>> GetRoutesDataAsync()
        {
            return await GetDataAsync<List<Models.ApiModels.TranzyV1Models.Route>>(_routesAPILink);
        }
        public async Task<List<Trip>> GetTripsDataAsync()
        {
            return await GetDataAsync<List<Trip>>(_tripsAPILink);
        }
        public async Task<List<Shape>> GetShapesDataAsync()
        {
            return await GetDataAsync<List<Shape>>(_shapesAPILink);
        }
        public async Task<List<Stop>> GetStopsDataAsync()
        {
            return await GetDataAsync<List<Stop>>(_stopsAPILink);
        }
        public async Task<List<StopTimes>> GetStopTimesDataAsync()
        {
            return await GetDataAsync<List<StopTimes>>(_stopTimesAPILink);
        }
    }
}
