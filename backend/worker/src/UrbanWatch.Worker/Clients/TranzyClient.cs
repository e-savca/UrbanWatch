using UrbanWatch.Worker.Models;

namespace UrbanWatch.Worker.Clients;

public class TranzyClient
{
    private readonly IHttpClientFactory _factory;

    public TranzyClient(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<Vehicle[]> GetVehiclesAsync(string agencyId, string apiKey)
    {
        using var client = _factory.CreateClient();

        try
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.tranzy.ai/v1/opendata/vehicles"),
                Headers =
                {
                    { "X-Agency-Id", agencyId },
                    { "Accept", "application/json" },
                    { "X-API-KEY", apiKey },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return Array.Empty<Vehicle>();
    }
}