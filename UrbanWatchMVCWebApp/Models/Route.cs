using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrbanWatchMVCWebApp.Models
{
    public class Route
    {
        public int Id { get; set; }
        [JsonProperty("route_id")] public int? RouteId { get; set; }
        [JsonProperty("agency_id")] public int? AgencyId { get; set; }
        [JsonProperty("route_short_name")] public string? RouteShortName { get; set; }
        [JsonProperty("route_long_name")] public string? RouteLongName { get; set; }
        [JsonProperty("route_color")] public string? RouteColor { get; set; }
        [JsonProperty("route_type")] public int? RouteType { get; set; }
    }

}
