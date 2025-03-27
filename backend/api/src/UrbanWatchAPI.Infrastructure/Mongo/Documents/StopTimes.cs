using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class StopTimes
{
    public ObjectId Id { get; set; }
    [JsonProperty("trip_id")]
    public string? TripId { get; set; }
    [JsonProperty("stop_id")]
    public int StopId { get; set; }
    [JsonProperty("stop_sequence")]
    public int StopSequence { get; set; }
}

