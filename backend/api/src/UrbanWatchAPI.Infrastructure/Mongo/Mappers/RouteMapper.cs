using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class RouteMapper : IDocumentMapper<Route, RouteDocument>
{
    public RouteDocument ToDocument(Route route)
    {
        return new RouteDocument()
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
    }

    public Route ToDomain(RouteDocument route)
    {
        return new Route()
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
    }
}