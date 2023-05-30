using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.DataTypes
{
    public class Shape
    {
        [JsonProperty("shape_id")] public string Id { get; set; } = "";
        [JsonProperty("shape_pt_lat")] public string Latitude { get; set; } = "";
        [JsonProperty("shape_pt_lon")] public string Longitude { get; set; } = "";
        [JsonProperty("shape_pt_sequence")] public int shapePointSequence { get; set; }
    }
}
