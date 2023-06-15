﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrbanWatchMVCWebApp.Models
{
    public class Route
    {
        public int Id { get; set; }
        [JsonProperty("route_id")] public string? RouteId { get; set; }
        [JsonProperty("agency_id")] public string? AgencyId { get; set; }
        [JsonProperty("route_short_name")] public string? RouteShortName { get; set; }
        [JsonProperty("route_long_name")] public string? RouteLongName { get; set; }
        [JsonProperty("route_color")] public string? RouteColor { get; set; }
        [JsonProperty("route_type")] public string? RouteType { get; set; }
    }

}
