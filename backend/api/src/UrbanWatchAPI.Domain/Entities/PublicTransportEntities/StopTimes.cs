namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class StopTimes
{
    public Guid Id { get; set; }
    public string? TripId { get; set; }
    public int StopId { get; set; }
    public int StopSequence { get; set; }
}