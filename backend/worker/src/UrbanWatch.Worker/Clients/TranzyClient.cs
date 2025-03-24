using System.Net;
using Newtonsoft.Json;
using UrbanWatch.Worker.ConfigManager;
using UrbanWatch.Worker.Models;

namespace UrbanWatch.Worker.Clients;

public class TranzyClient
{
    public EnvManager EnvManager { get; }
    private readonly IHttpClientFactory _factory;

    private readonly ILogger<TranzyClient> _logger;

    public TranzyClient(
        EnvManager envManager,
        IHttpClientFactory factory,
        ILogger<TranzyClient> logger)
    {
        EnvManager = envManager;
        _factory = factory;
        _logger = logger;
    }

    public async Task<List<Vehicle>> GetVehiclesAsync(string agencyId)
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
                    { "X-API-KEY", EnvManager.TranzyApiKeyManager.GetCurrentKey() },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    _logger.LogWarning("Status Code 429: Too Many Requests");
                    bool switched = EnvManager.TranzyApiKeyManager.TrySwitchKey();
                    if (!switched)
                        throw new Exception("All API keys exceeded quota.");
                }

                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Vehicle>>(body) ?? new List<Vehicle>();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}