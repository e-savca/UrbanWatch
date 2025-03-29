using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class StopMapper : IDocumentMapper<Stop, StopDocument>
{
    public StopDocument ToDocument(Stop stop)
    {
        return new StopDocument();
    }

    public Stop ToDomain(StopDocument stop)
    {
        return new Stop();
    }
}