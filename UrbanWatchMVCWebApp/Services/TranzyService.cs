using Newtonsoft.Json;
using UrbanWatchMVCWebApp.DataTypes;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyService
    {
        public Vehicle[] GetData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.tranzy.dev/v1/opendata/vehicles"),
                Headers =
                {
                    { "X-Agency-Id", "4" },
                    { "X-API-KEY", "vPb5jKPyRf1AS3AWrqYRL5hUHjxViy2u202HJ3h3" },
                },
            };
            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                Vehicle[] vehicles = JsonConvert.DeserializeObject<Vehicle[]>(body);
                return vehicles;
            }
        }
    }
}
