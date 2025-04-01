using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class AgencyMapper : IDocumentMapper<Agency, AgencyDocument>
{
    public AgencyDocument ToDocument(Agency agency)
    {
        return new AgencyDocument
        {
            Id = agency.Id,
            AgencyId = agency.AgencyId,
            AgencyName = agency.AgencyName,
            AgencyUrl = agency.AgencyUrl,
            AgencyTimezone = agency.AgencyTimezone,
            AgencyLang = agency.AgencyLang,
            AgencyFareUrl = agency.AgencyFareUrl
        };
    }

    public Agency ToDomain(AgencyDocument agency)
    {
        return new Agency
        {
            Id = agency.Id,
            AgencyId = agency.AgencyId,
            AgencyName = agency.AgencyName,
            AgencyUrl = agency.AgencyUrl,
            AgencyTimezone = agency.AgencyTimezone,
            AgencyLang = agency.AgencyLang,
            AgencyFareUrl = agency.AgencyFareUrl
        };
    }
}