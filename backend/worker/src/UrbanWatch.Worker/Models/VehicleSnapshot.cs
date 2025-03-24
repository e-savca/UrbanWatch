using MongoDB.Bson;

namespace UrbanWatch.Worker.Models;

public class VehicleSnapshot
{
    public ObjectId Id { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new();
}