namespace UrbanWatchAPI.Domain.Entities.PublicTransportEntities;

public class Route
{
    public string? AgencyId { get; set; }
    public int RouteId { get; set; }
    public string? RouteShortName { get; set; }
    public string? RouteLongName { get; set; }
    public string? RouteColor { get; set; }
    public int RouteType { get; set; }
    public string? RouteDesc { get; set; }
}