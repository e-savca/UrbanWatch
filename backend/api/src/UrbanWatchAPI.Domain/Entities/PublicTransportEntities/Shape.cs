namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class Shape
{
    public Guid Id { get; set; }
    public string? ShapeId { get; set; }
    public double ShapePtLat { get; set; }
    public double ShapePtLon { get; set; }
    public int ShapePtSequence { get; set; }
}