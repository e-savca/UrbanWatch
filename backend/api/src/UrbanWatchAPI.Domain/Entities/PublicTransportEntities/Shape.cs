namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class Shape
{
    public string? ShapeId { get; set; }
    public double ShapePtLat { get; set; }
    public double ShapePtLon { get; set; }
    public int ShapePtSequence { get; set; }
}