using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.DataTypes
{
    public class ExtendedVehicle
    {
        // Vehicle properties
        [JsonProperty("id")] public string Id { get; set; } = "";
        [JsonProperty("label")] public string Label { get; set; } = "";
        [JsonProperty("latitude")] public string Latitude { get; set; } = "";
        [JsonProperty("longitude")] public string Longitude { get; set; } = "";
        [JsonProperty("timestamp")] public string Timestamp { get; set; } = "";
        [JsonProperty("vehicle_type")] public string vehicleType { get; set; } = "";
        [JsonProperty("bike_accessible")] public string bikeAccessible { get; set; } = "";
        [JsonProperty("wheelchair_accessible")] public string wheelchairAccessible { get; set; } = "";
        [JsonProperty("x_provider")] public string xProvider { get; set; } = "";
        [JsonProperty("x_rand")] public string xRand { get; set; } = "";
        [JsonProperty("speed")] public string Speed { get; set; } = "";
        [JsonProperty("trip_id")] public string tripId { get; set; } = "";

        // Route properties
        [JsonProperty("agency_id")] public int agencyId { get; set; } = 0;
        [JsonProperty("route_id")] public string routeId { get; set; } = "";
        [JsonProperty("route_short_name")] public string routeShortName { get; set; } = "";
        [JsonProperty("route_long_name")] public string routeLongName { get; set; } = "";
        [JsonProperty("route_color")] public string routeColor { get; set; } = "";
        [JsonProperty("route_type")] public int routeType { get; set; } = 0;
        public void AddVehicleData(Vehicle vehicle)
        {
            this.Id = vehicle.Id;
            this.Label = vehicle.Label;
            this.Latitude = vehicle.Latitude;
            this.Longitude = vehicle.Longitude;
            this.Timestamp = vehicle.Timestamp;
            this.vehicleType = vehicle.vehicleType;
            this.bikeAccessible = vehicle.bikeAccessible;
            this.wheelchairAccessible = vehicle.wheelchairAccessible;
            this.xProvider = vehicle.xProvider;
            this.xRand = vehicle.xRand;
            this.Speed = vehicle.Speed;
            this.tripId = vehicle.tripId;
            this.routeId = vehicle.routeId;
        }
        public void AddRouteData(Route route)
        {
            this.agencyId = route.agencyId;
            //this.routeId = route.routeId;
            this.routeShortName = route.routeShortName;
            this.routeLongName = route.routeLongName;
            this.routeColor = route.routeColor;
            this.routeType = route.routeType;
        }
    }
}
