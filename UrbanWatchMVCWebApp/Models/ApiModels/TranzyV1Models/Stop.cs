using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;
public class Stop
{
    [JsonProperty("stop_id")] public string? StopId { get; set; }
    [JsonProperty("stop_name")] public string? Name { get; set; }
    [JsonProperty("stop_lat")] public string? Latitude { get; set; }
    [JsonProperty("stop_lon")] public string? Longitude { get; set; }
    [JsonProperty("location_type")] public string? LocationType { get; set; }
}