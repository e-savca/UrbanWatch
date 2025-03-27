using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class Shape
{
    public ObjectId Id { get; set; }
    [JsonProperty("shape_id")]
    public string? ShapeId { get; set; }
    [JsonProperty("shape_pt_lat")]
    public double ShapePtLat { get; set; }
    [JsonProperty("shape_pt_lon")]
    public double ShapePtLon { get; set; }
    [JsonProperty("shape_pt_sequence")]
    public int ShapePtSequence { get; set; }
}

