using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class TripMapper
{
    public TripDocument ToDocument(Trip trip)
    {
        return new TripDocument();
    }

    public Trip ToDomain(TripDocument trip)
    {
        return new Trip();
    }
}