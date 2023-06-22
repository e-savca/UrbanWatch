namespace UrbanWatchMVCWebApp.Models.DataModels;
public class Trip
{
    public int Id { get; set; }
    public string? TripId { get; set; }
    public string? RouteId { get; set; }
    public string? TripHeadsign { get; set; }
    public string? DirectionId { get; set; }
    public string? BlockId { get; set; }
    public string? ShapeId { get; set; }
}