using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class TripDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public int RouteId { get; set; }
    public string? TripId { get; set; }
    public string? TripHeadsign { get; set; }
    public int DirectionId { get; set; }
    public int BlockId { get; set; }
    public string? ShapeId { get; set; }
}

