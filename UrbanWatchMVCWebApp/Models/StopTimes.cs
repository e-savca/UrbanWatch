using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrbanWatchMVCWebApp.Models
{
    public class StopTimes
    {
        public int Id { get; set; }
        [JsonProperty("trip_id")] public string? TripId { get; set; }
        [JsonProperty("stop_id")] public string? StopId { get; set; }
        [JsonProperty("stop_sequence")] public string? StopSequence { get; set; }
    }
}
