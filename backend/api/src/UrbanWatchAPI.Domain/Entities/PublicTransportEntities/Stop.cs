namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class Stop
{
    public Guid Id { get; set; }
    public int StopId { get; set; }
    public string? StopName { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public int LocationType { get; set; }
    public string? Code { get; set; }
}