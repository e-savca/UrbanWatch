using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class RouteMapper : IDocumentMapper<Route, RouteDocument>
{
    public RouteDocument ToDocument(Route route)
    {
        return new RouteDocument();
    }

    public Route ToDomain(RouteDocument route)
    {
        return new Route();
    }
}