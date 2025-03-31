using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class ShapeDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string? ShapeId { get; set; }
    public double ShapePtLat { get; set; }
    public double ShapePtLon { get; set; }
    public int ShapePtSequence { get; set; }
}

