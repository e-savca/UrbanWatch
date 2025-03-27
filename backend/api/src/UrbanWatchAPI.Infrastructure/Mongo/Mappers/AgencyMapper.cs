using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class AgencyMapper
{
    public AgencyDocument ToDocument(Agency agency)
    {
        return new AgencyDocument();
    }

    public Agency ToDomain(AgencyDocument agency)
    {
        return new Agency();
    }
}