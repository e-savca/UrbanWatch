namespace UrbanWatchMVCWebApp.Models.DataModels;
public class StopTimes
{
    public int Id { get; set; }
    public string? TripId { get; set; }
    public string? StopId { get; set; }
    public string? StopSequence { get; set; }
}
