namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class Trip
{
    public Guid Id { get; set; }
    public int RouteId { get; set; }
    public string? TripId { get; set; }
    public string? TripHeadsign { get; set; }
    public int DirectionId { get; set; }
    public int BlockId { get; set; }
    public string? ShapeId { get; set; }
}