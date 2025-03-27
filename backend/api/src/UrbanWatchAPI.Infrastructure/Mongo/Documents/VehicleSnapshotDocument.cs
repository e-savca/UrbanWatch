using MongoDB.Bson;

namespace UrbanWatchAPI.Infrastructure.Mongo.Documents;

public class VehicleSnapshotDocument
{
    public ObjectId Id { get; set; }
    public DateTime Timestamp { get; set; }
    public List<VehicleDocument> Vehicles { get; set; } = new();
}