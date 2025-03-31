using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class AgencyDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public int AgencyId { get; set; }
    public string? AgencyName { get; set; }
    public string? AgencyUrl { get; set; }
    public string? AgencyTimezone { get; set; }
    public string? AgencyLang { get; set; }
    public string? AgencyFareUrl { get; set; }
}

