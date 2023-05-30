using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.DataTypes
{
    public class Vehicle
    {
        [JsonProperty("id")] public string Id { get; set; } = "";
        [JsonProperty("label")] public string Label { get; set; } = "";
        [JsonProperty("latitude")] public string Latitude { get; set; } = "";
        [JsonProperty("longitude")] public string Longitude { get; set; } = "";
        [JsonProperty("timestamp")] public string Timestamp { get; set; } = "";
        [JsonProperty("vehicle_type")] public string vehicleType { get; set; } = "";
        [JsonProperty("bike_accessible")] public string bikeAccessible { get; set; } = "";
        [JsonProperty("wheelchair_accessible")] public string wheelchairAccessible { get; set; } = "";
        [JsonProperty("x_provider")] public string xProvider { get; set; } = "";
        [JsonProperty("x_rand")] public string xRand { get; set; } = "";
        [JsonProperty("speed")] public string Speed { get; set; } = "";
        [JsonProperty("route_id")] public int? routeId { get; set; }
        [JsonProperty("trip_id")] public string tripId { get; set; } = "";
    }
}
