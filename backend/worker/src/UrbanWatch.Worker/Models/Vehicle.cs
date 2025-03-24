using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatch.Worker.Models;

public class Vehicle
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [JsonProperty("id")]
    [BsonElement("vehicle_id")]
    public string? VehicleId { get; set; }

    [JsonProperty("label")]
    public string? Label { get; set; }

    [JsonProperty("latitude")]
    public string? Latitude { get; set; }

    [JsonProperty("longitude")]
    public string? Longitude { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("vehicle_type")]
    public int VehicleType { get; set; }

    [JsonProperty("bike_accessible")]
    public string? BikeAccessible { get; set; }

    [JsonProperty("wheelchair_accessible")]
    public string? WheelchairAccessible { get; set; }

    [JsonProperty("x_provider")]
    public string? XProvider { get; set; }

    [JsonProperty("x_rand")]
    public string? XRand { get; set; }

    [JsonProperty("speed")]
    public string? Speed { get; set; }

    [JsonProperty("route_id")]
    public string? RouteId { get; set; }

    [JsonProperty("trip_id")]
    public string? TripId { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
