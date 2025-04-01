using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;
public class StopTimes
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [JsonProperty("trip_id")]
    public string? TripId { get; set; }
    [JsonProperty("stop_id")]
    public int StopId { get; set; }
    [JsonProperty("stop_sequence")]
    public int StopSequence { get; set; }
}

