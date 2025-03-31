using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class StopDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public int StopId { get; set; }
    public string? StopName { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public int LocationType { get; set; }
    public string? Code { get; set; }
}

