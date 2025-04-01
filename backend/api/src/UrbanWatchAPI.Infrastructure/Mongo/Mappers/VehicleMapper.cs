using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class VehicleMapper : IDocumentMapper<Vehicle, VehicleDocument>
{
    public VehicleDocument ToDocument(Vehicle vehicle)
    {
        return new VehicleDocument(){
            VehicleId = vehicle.VehicleId,
            Label = vehicle.Label,
            Latitude = vehicle.Latitude,
            Longitude = vehicle.Longitude,
            Timestamp = vehicle.Timestamp,
            VehicleType = vehicle.VehicleType,
            BikeAccessible = vehicle.BikeAccessible,
            WheelchairAccessible = vehicle.WheelchairAccessible,
            Speed = vehicle.Speed,
            RouteId = vehicle.RouteId,
            TripId = vehicle.TripId
        };
    }

    public Vehicle ToDomain(VehicleDocument vehicle)
    {
        return new Vehicle(){
            VehicleId = vehicle.VehicleId,
            Label = vehicle.Label,
            Latitude = vehicle.Latitude,
            Longitude = vehicle.Longitude,
            Timestamp = vehicle.Timestamp,
            VehicleType = vehicle.VehicleType,
            BikeAccessible = vehicle.BikeAccessible,
            WheelchairAccessible = vehicle.WheelchairAccessible,
            Speed = vehicle.Speed,
            RouteId = vehicle.RouteId,
            TripId = vehicle.TripId
        };
    }
}