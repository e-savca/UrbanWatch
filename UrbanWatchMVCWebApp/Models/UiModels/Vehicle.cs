using UrbanWatchMVCWebApp.Models.Enums;

namespace UrbanWatchMVCWebApp.Models.UiModels;
public class Vehicle
{
    public string? VehicleId { get; set; }
    public string? Label { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public DateTime Timestamp { get; set; }
    public RouteType? VehicleType { get; set; }
    public string? BikeAccessible { get; set; }
    public string? WheelchairAccessible { get; set; }
    public string? XProvider { get; set; }
    public string? XRand { get; set; }
    public string? Speed { get; set; }
    public string? RouteId { get; set; }
    public string? TripId { get; set; }

    public override string ToString()
    {
        return $"VehicleId: {VehicleId}, Label: {Label}, Latitude: {Latitude}, Longitude: {Longitude}, Timestamp: {Timestamp}, VehicleType: {VehicleType}, BikeAccessible: {BikeAccessible}, WheelchairAccessible: {WheelchairAccessible}, XProvider: {XProvider}, XRand: {XRand}, Speed: {Speed}, RouteId: {RouteId}, TripId: {TripId}";
    }
}
