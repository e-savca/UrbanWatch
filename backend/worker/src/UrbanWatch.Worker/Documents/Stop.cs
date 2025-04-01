using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;

public class Stop
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [JsonProperty("stop_id")]
    public int StopId { get; set; }
    [JsonProperty("stop_name")]
    public string? StopName { get; set; }
    [JsonProperty("stop_lat")]
    public double Lat { get; set; }
    [JsonProperty("stop_lon")]
    public double Lon { get; set; }
    [JsonProperty("location_type")]
    public int LocationType { get; set; }
    [JsonProperty("stop_code")]
    public string? Code { get; set; }
}

