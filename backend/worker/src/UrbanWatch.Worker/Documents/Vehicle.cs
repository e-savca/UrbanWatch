using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;

public class Vehicle
{
    [JsonProperty("id")]
    public string? VehicleId { get; set; }

    [JsonProperty("label")]
    public string? Label { get; set; }

    [JsonProperty("latitude")]
    public string? Latitude { get; set; }

    [JsonProperty("longitude")]
    public string? Longitude { get; set; }

    [JsonProperty("timestamp")]
    public string? Timestamp { get; set; }

    [JsonProperty("vehicle_type")]
    public int VehicleType { get; set; }

    [JsonProperty("bike_accessible")]
    public string? BikeAccessible { get; set; }

    [JsonProperty("wheelchair_accessible")]
    public string? WheelchairAccessible { get; set; }

    [JsonProperty("speed")]
    public string? Speed { get; set; }

    [JsonProperty("route_id")]
    public string? RouteId { get; set; }

    [JsonProperty("trip_id")]
    public string? TripId { get; set; }
}
