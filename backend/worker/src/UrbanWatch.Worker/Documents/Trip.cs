using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;
public class Trip
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [JsonProperty("route_id")]
    public int RouteId { get; set; }
    [JsonProperty("trip_id")]
    public string? TripId { get; set; }
    [JsonProperty("trip_headsign")]
    public string? TripHeadsign { get; set; }
    [JsonProperty("direction_id")]
    public int DirectionId { get; set; }
    [JsonProperty("block_id")]
    public int BlockId { get; set; }
    [JsonProperty("shape_id")]
    public string? ShapeId { get; set; }
}

