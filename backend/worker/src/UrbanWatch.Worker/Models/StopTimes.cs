using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;
public class StopTimes
{
    [JsonProperty("trip_id")]
    public string? TripId { get; set; }
    [JsonProperty("stop_id")]
    public int StopId { get; set; }
    [JsonProperty("stop_sequence")]
    public int StopSequence { get; set; }
}

