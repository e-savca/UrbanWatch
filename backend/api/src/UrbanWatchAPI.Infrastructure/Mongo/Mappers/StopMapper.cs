using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class StopMapper : IDocumentMapper<Stop, StopDocument>
{
    public StopDocument ToDocument(Stop stop)
    {
        return new StopDocument(){
            Id = stop.Id,
            StopId = stop.StopId,
            StopName = stop.StopName,
            Lat = stop.Lat,
            Lon = stop.Lon,
            Code = stop.Code,
            LocationType = stop.LocationType,
        };
    }

    public Stop ToDomain(StopDocument stop)
    {
        return new Stop(){
            Id = stop.Id,
            StopId = stop.StopId,
            StopName = stop.StopName,
            Lat = stop.Lat,
            Lon = stop.Lon,
            Code = stop.Code,
            LocationType = stop.LocationType,
        };
    }
}