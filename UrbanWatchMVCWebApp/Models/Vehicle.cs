﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrbanWatchMVCWebApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [JsonProperty("id")] public string? VehicleId { get; set; }
        [JsonProperty("label")] public string? Label { get; set; }
        [JsonProperty("latitude")] public string? Latitude { get; set; }
        [JsonProperty("longitude")] public string? Longitude { get; set; }
        [JsonProperty("timestamp")] public string? Timestamp { get; set; }
        [JsonProperty("vehicle_type")] public string? VehicleType { get; set; }
        [JsonProperty("bike_accessible")] public string? BikeAccessible { get; set; }
        [JsonProperty("wheelchair_accessible")] public string? WheelchairAccessible { get; set; }
        [JsonProperty("x_provider")] public string? XProvider { get; set; }
        [JsonProperty("x_rand")] public string? XRand { get; set; }
        [JsonProperty("speed")] public string? Speed { get; set; }
        [JsonProperty("route_id")] public string? RouteId { get; set; }
        [JsonProperty("trip_id")] public string? TripId { get; set; }
    }
}
