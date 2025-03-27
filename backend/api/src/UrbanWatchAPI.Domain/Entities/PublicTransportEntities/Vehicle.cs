namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class Vehicle
{
    public string? VehicleId { get; set; }

    public string? Label { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? Timestamp { get; set; }

    public int VehicleType { get; set; }

    public string? BikeAccessible { get; set; }

    public string? WheelchairAccessible { get; set; }

    public string? Speed { get; set; }

    public string? RouteId { get; set; }

    public string? TripId { get; set; }
}