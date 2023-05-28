using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.DataTypes
{
    public class Shape
    {
        [JsonProperty("shape_id")] public string shapeId { get; set; } = "";
        [JsonProperty("shape_pt_lat")] public string shapePointLat { get; set; } = "";
        [JsonProperty("shape_pt_lon")] public string shapePointLon { get; set; } = "";
        [JsonProperty("shape_pt_sequence")] public int shapePointSequence { get; set; }
    }
}
