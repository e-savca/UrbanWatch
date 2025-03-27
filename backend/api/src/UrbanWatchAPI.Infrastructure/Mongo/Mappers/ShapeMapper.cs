using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class ShapeMapper
{
    public ShapeDocument ToDocument(Shape shape)
    {
        return new ShapeDocument();
    }

    public Shape ToDomain(ShapeDocument shape)
    {
        return new Shape();
    }
}