using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using NuGet.Packaging.Licenses;
using System.Drawing.Text;
using UrbanWatchMVCWebApp.DataTypes;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyService
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
        private string GetData(Uri RequestUri)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = RequestUri,
                Headers =
                {
                    { "X-Agency-Id", _AgencyId },
                    { "X-API-KEY", _APIKey },
                },
            };
            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        public Vehicle[] GetVehiclesData()
        {
            var body = GetData(_vehiclesAPILink);
            return JsonConvert.DeserializeObject<Vehicle[]>(body);
        }
        public DataTypes.Route[] GetRoutesData()
        {
            var body = GetData(_routesAPILink);
            return JsonConvert.DeserializeObject<DataTypes.Route[]>(body);
        }
        public Trip[] GetTripsData()
        {
            var body = GetData(_tripsAPILink);
            return JsonConvert.DeserializeObject<Trip[]>(body);
        }
        public Shape[] GetShapesData()
        {
            var body = GetData(_shapesAPILink);
            return JsonConvert.DeserializeObject<Shape[]>(body);
        }
        public Stop[] GetStopsData()
        {
            var body = GetData(_stopsAPILink);
            return JsonConvert.DeserializeObject<Stop[]>(body);
        }
        public StopTimes[] GetStopTimesData()
        {
            var body = GetData(_stopTimesAPILink);
            return JsonConvert.DeserializeObject<StopTimes[]>(body);
        }
    }
}
