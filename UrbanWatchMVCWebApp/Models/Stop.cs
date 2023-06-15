using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrbanWatchMVCWebApp.Models
{
    public class Stop
    {
        public int Id { get; set; }
        [JsonProperty("stop_id")] public int? StopId { get; set; }
        [JsonProperty("stop_name")] public string? Name { get; set; }
        [JsonProperty("stop_lat")] public string? Latitude { get; set; }
        [JsonProperty("stop_lon")] public string? Longitude { get; set; }
        [JsonProperty("location_type")] public int? LocationType { get; set; }
    }
}
