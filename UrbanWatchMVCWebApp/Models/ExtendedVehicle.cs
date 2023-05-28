using Newtonsoft.Json;
using UrbanWatchMVCWebApp.Models.DataTypes;

namespace UrbanWatchMVCWebApp.Models
{
    public class ExtendedVehicle
    {
        // Vehicle properties
        public string Id { get; set; }
        public string Label { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Timestamp { get; set; }
        public string vehicleType { get; set; }
        public string bikeAccessible { get; set; }
        public string wheelchairAccessible { get; set; }
        public string xProvider { get; set; }
        public string xRand { get; set; }
        public string Speed { get; set; }
        public string routeId { get; set; }
        public string tripId { get; set; }

        // Route properties
        public int agencyId { get; set; } = 0;
        public string routeShortName { get; set; } = "";
        public string routeLongName { get; set; } = "";
        public string routeColor { get; set; } = "";
        public int routeType { get; set; } = 0;
        public ExtendedVehicle(Vehicle vehicle)
        {
            Id = vehicle.Id;
            Label = vehicle.Label;
            Latitude = vehicle.Latitude;
            Longitude = vehicle.Longitude;
            Timestamp = vehicle.Timestamp;
            vehicleType = vehicle.vehicleType;
            bikeAccessible = vehicle.bikeAccessible;
            wheelchairAccessible = vehicle.wheelchairAccessible;
            xProvider = vehicle.xProvider;
            xRand = vehicle.xRand;
            Speed = vehicle.Speed;
            routeId = vehicle.routeId;
            tripId = vehicle.tripId;
        }
        public void AddRouteData(DataTypes.Route route)
        {
            agencyId = route.agencyId;
            routeShortName = route.routeShortName;
            routeLongName = route.routeLongName;
            routeColor = route.routeColor;
            routeType = route.routeType;
        }
    }
}
