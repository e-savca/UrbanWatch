﻿using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;

public class Trip
{
    [JsonProperty("trip_id")] public string? TripId { get; set; }
    [JsonProperty("route_id")] public string? RouteId { get; set; }
    [JsonProperty("trip_headsign")] public string? TripHeadsign { get; set; }
    [JsonProperty("direction_id")] public string? DirectionId { get; set; }
    [JsonProperty("block_id")] public string? BlockId { get; set; }
    [JsonProperty("shape_id")] public string? ShapeId { get; set; }
}

