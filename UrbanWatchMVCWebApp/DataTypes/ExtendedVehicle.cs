using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.DataTypes
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
            this.routeId = vehicle.routeId;
            this.tripId = vehicle.tripId;            
        }
        public void AddRouteData(Route route)
        {
            this.agencyId = route.agencyId;
            this.routeShortName = route.routeShortName;
            this.routeLongName = route.routeLongName;
            this.routeColor = route.routeColor;
            this.routeType = route.routeType;
        }
    }
}
