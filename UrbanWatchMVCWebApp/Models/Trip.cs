using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrbanWatchMVCWebApp.Models
{
    public class Trip
    {
        public int Id { get; set; }
        [JsonProperty("trip_id")] public string? TripId { get; set; }
        [JsonProperty("route_id")] public int? RouteId { get; set; }
        [JsonProperty("trip_headsign")] public string? TripHeadsign { get; set; }
        [JsonProperty("direction_id")] public int? DirectionId { get; set; }
        [JsonProperty("block_id")] public int? BlockId { get; set; }
        [JsonProperty("shape_id")] public string? ShapeId { get; set; }
    }
}
