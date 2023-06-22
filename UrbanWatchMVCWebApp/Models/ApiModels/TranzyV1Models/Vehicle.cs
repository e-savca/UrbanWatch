using Newtonsoft.Json;
using UrbanWatchMVCWebApp.Models.Enums;

namespace UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;

public class Vehicle
{
    [JsonProperty("id")] public string? VehicleId { get; set; }
    [JsonProperty("label")] public string? Label { get; set; }
    [JsonProperty("latitude")] public string? Latitude { get; set; }
    [JsonProperty("longitude")] public string? Longitude { get; set; }
    [JsonProperty("timestamp")] public DateTime Timestamp { get; set; }
    [JsonProperty("vehicle_type")] public RouteType? VehicleType { get; set; }
    [JsonProperty("bike_accessible")] public string? BikeAccessible { get; set; }
    [JsonProperty("wheelchair_accessible")] public string? WheelchairAccessible { get; set; }
    [JsonProperty("x_provider")] public string? XProvider { get; set; }
    [JsonProperty("x_rand")] public string? XRand { get; set; }
    [JsonProperty("speed")] public string? Speed { get; set; }
    [JsonProperty("route_id")] public string? RouteId { get; set; }
    [JsonProperty("trip_id")] public string? TripId { get; set; }
}
