using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;
public class StopTimes
{
    [JsonProperty("trip_id")] public string? TripId { get; set; }
    [JsonProperty("stop_id")] public string? StopId { get; set; }
    [JsonProperty("stop_sequence")] public string? StopSequence { get; set; }
}