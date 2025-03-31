using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Domain.Interfaces;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo.Mappers;

public class AgencyMapper : IDocumentMapper<Agency, AgencyDocument>
{
    public AgencyDocument ToDocument(Agency agency)
    {
        var document = new AgencyDocument
        {
            Id = agency.Id,
            AgencyId = agency.AgencyId,
            AgencyName = agency.AgencyName,
            AgencyUrl = agency.AgencyUrl,
            AgencyTimezone = agency.AgencyTimezone,
            AgencyLang = agency.AgencyLang,
            AgencyFareUrl = agency.AgencyFareUrl
        };

        return document;
    }

    public Agency ToDomain(AgencyDocument document)
    {
        var agency = new Agency
        {
            Id = document.Id,
            AgencyId = document.AgencyId,
            AgencyName = document.AgencyName,
            AgencyUrl = document.AgencyUrl,
            AgencyTimezone = document.AgencyTimezone,
            AgencyLang = document.AgencyLang,
            AgencyFareUrl = document.AgencyFareUrl
        };

        return agency;
    }
}