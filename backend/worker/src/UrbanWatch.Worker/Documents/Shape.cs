using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;

public class Shape
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [JsonProperty("shape_id")]
    public string? ShapeId { get; set; }
    [JsonProperty("shape_pt_lat")]
    public double ShapePtLat { get; set; }
    [JsonProperty("shape_pt_lon")]
    public double ShapePtLon { get; set; }
    [JsonProperty("shape_pt_sequence")]
    public int ShapePtSequence { get; set; }
}

