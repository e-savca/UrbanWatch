using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class StopTimesDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string? TripId { get; set; }
    public int StopId { get; set; }
    public int StopSequence { get; set; }
}

