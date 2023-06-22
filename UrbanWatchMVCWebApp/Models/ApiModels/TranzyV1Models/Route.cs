using Newtonsoft.Json;
using UrbanWatchMVCWebApp.Models.Enums;

namespace UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;

public class Route
{
    [JsonProperty("route_id")] public string? RouteId { get; set; }
    [JsonProperty("agency_id")] public string? AgencyId { get; set; }
    [JsonProperty("route_short_name")] public string? RouteShortName { get; set; }
    [JsonProperty("route_long_name")] public string? RouteLongName { get; set; }
    [JsonProperty("route_color")] public string? RouteColor { get; set; }
    [JsonProperty("route_type")] public RouteType? RouteType { get; set; }
}