using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class VehicleSnapshotDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public List<VehicleDocument> Vehicles { get; set; } = new();
}