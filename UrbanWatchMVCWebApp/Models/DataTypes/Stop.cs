﻿using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.DataTypes
{
    public class Stop
    {
        [JsonProperty("stop_id")] public int stopId { get; set; }
        [JsonProperty("stop_name")] public string stopName { get; set; } = "";
        [JsonProperty("stop_lat")] public string stopLat { get; set; } = "";
        [JsonProperty("stop_lon")] public string stopLon { get; set; } = "";
        [JsonProperty("location_type")] public int locationType { get; set; }
    }
}
