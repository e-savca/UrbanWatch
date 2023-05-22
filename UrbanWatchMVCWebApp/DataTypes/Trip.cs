using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.DataTypes
{
    public class Trip
    {
        [JsonProperty("route_id")] public int routeId { get; set; }
        [JsonProperty("trip_id")] public string tripId { get; set; } = "";
        [JsonProperty("trip_headsign")] public string tripHeadsign { get; set; } = "";
        [JsonProperty("direction_id")] public int directionId { get; set; }
        [JsonProperty("block_id")] public int blockId { get; set; }
        [JsonProperty("shape_id")] public string shapeId { get; set; } = "";
    }
}
