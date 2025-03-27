using MongoDB.Bson;
using Newtonsoft.Json;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class Route
{
    public ObjectId Id { get; set; }
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

