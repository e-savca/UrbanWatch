using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrbanWatch.Worker.Models;

public class VehicleSnapshot
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new();
}