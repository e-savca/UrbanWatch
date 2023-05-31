using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models
{
    public class StopTimes
    {
        [JsonProperty("trip_id")] public string? tripId { get; set; }
        [JsonProperty("stop_id")] public int? stopId { get; set; }
        [JsonProperty("stop_sequence")] public int? stopSequence { get; set; }
    }
}
