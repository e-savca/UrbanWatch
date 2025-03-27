using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;

public class Shape
{
    [JsonProperty("shape_id")]
    public string? ShapeId { get; set; }
    [JsonProperty("shape_pt_lat")]
    public double ShapePtLat { get; set; }
    [JsonProperty("shape_pt_lon")]
    public double ShapePtLon { get; set; }
    [JsonProperty("shape_pt_sequence")]
    public int ShapePtSequence { get; set; }
}

