using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrbanWatchMVCWebApp.Models
{
    public class Shape
    {
        public int Id { get; set; }
        [JsonProperty("shape_id")] public string? ShapeId { get; set; }
        [JsonProperty("shape_pt_lat")] public string? Latitude { get; set; }
        [JsonProperty("shape_pt_lon")] public string? Longitude { get; set; }
        [JsonProperty("shape_pt_sequence")] public string? ShapePointSequence { get; set; }
    }
}
