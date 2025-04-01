using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class ShapeMapper
{
    public ShapeDocument ToDocument(Shape shape)
    {
        return new ShapeDocument(){
            Id = shape.Id,
            ShapeId = shape.ShapeId,
            ShapePtLat = shape.ShapePtLat,
            ShapePtLon = shape.ShapePtLon,
            ShapePtSequence = shape.ShapePtSequence
        };
    }

    public Shape ToDomain(ShapeDocument shape)
    {
        return new Shape(){
            Id = shape.Id,
            ShapeId = shape.ShapeId,
            ShapePtLat = shape.ShapePtLat,
            ShapePtLon = shape.ShapePtLon,
            ShapePtSequence = shape.ShapePtSequence
        };
    }
}