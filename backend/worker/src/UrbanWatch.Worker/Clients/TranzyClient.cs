using System.Net;
using Newtonsoft.Json;
using UrbanWatch.Worker.Models;
using UrbanWatch.Worker.Services;

namespace UrbanWatch.Worker.Clients;

public class TranzyClient
{
    private readonly ApiKeyManager _apiKeyManager;
    private readonly IHttpClientFactory _factory;

    private readonly ILogger<TranzyClient> _logger;

    public TranzyClient(
        ApiKeyManager apiKeyManager,
        IHttpClientFactory factory,
        ILogger<TranzyClient> logger)
    {
        _apiKeyManager = apiKeyManager;
        _factory = factory;
        _logger = logger;
    }

    public async Task<Vehicle[]> GetVehiclesAsync(string agencyId)
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
                    { "X-API-KEY", _apiKeyManager.GetCurrentKey() },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    _logger.LogWarning("Status Code 429: Too Many Requests");
                    bool switched = _apiKeyManager.TrySwitchKey();
                    if (!switched)
                        throw new Exception("All API keys exceeded quota.");
                }

                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Vehicle[]>(body) ?? Array.Empty<Vehicle>();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}