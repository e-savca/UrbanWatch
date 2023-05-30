﻿using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.DataTypes
{
    public class Route
    {
        [JsonProperty("agency_id")] public int agencyId { get; set; }
        [JsonProperty("route_id")] public int Id { get; set; }
        [JsonProperty("route_short_name")] public string routeShortName { get; set; } = "";
        [JsonProperty("route_long_name")] public string routeLongName { get; set; } = "";
        [JsonProperty("route_color")] public string routeColor { get; set; } = "";
        [JsonProperty("route_type")] public int routeType { get; set; }
    }

}
