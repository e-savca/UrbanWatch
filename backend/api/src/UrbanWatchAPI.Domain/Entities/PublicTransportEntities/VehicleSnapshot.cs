namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class VehicleSnapshot
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}