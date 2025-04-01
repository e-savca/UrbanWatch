using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace UrbanWatch.Worker.Models;

public class Route
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [JsonProperty("agency_id")]
    public string? AgencyId { get; set; }
    [JsonProperty("route_id")]
    public int RouteId { get; set; }
    [JsonProperty("route_short_name")]
    public string? RouteShortName { get; set; }
    [JsonProperty("route_long_name")]
    public string? RouteLongName { get; set; }
    [JsonProperty("route_color")]
    public string? RouteColor { get; set; }
    [JsonProperty("route_type")]
    public int RouteType { get; set; }
    [JsonProperty("route_desc")]
    public string? RouteDesc { get; set; }
}

