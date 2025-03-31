using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class RouteMapper : IDocumentMapper<Route, RouteDocument>
{
    public RouteDocument ToDocument(Route route)
    {
        var document = new RouteDocument()
        {
            Id = route.Id,
            AgencyId = route.AgencyId,
            RouteId = route.RouteId,
            RouteShortName = route.RouteShortName,
            RouteLongName = route.RouteLongName,
            RouteColor = route.RouteColor,
            RouteType = route.RouteType,
            RouteDesc = route.RouteDesc
        };
        return document;
    }

    public Route ToDomain(RouteDocument document)
    {
        var route = new Route()
        {
            Id = document.Id,
            AgencyId = document.AgencyId,
            RouteId = document.RouteId,
            RouteShortName = document.RouteShortName,
            RouteLongName = document.RouteLongName,
            RouteColor = document.RouteColor,
            RouteType = document.RouteType,
            RouteDesc = document.RouteDesc
        };
        return route;
    }
}