using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class StopMapper
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