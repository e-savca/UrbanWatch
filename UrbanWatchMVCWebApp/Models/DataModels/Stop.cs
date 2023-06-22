namespace UrbanWatchMVCWebApp.Models.DataModels;

public class Stop
{
    public int Id { get; set; }
    public string? StopId { get; set; }
    public string? Name { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? LocationType { get; set; }
}
