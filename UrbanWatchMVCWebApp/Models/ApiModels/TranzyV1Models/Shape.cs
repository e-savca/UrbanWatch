using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;

public class Shape
{
    [JsonProperty("shape_id")] public string? ShapeId { get; set; }
    [JsonProperty("shape_pt_lat")] public string? Latitude { get; set; }
    [JsonProperty("shape_pt_lon")] public string? Longitude { get; set; }
    [JsonProperty("shape_pt_sequence")] public string? ShapePointSequence { get; set; }
}