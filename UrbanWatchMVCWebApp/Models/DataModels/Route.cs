using UrbanWatchMVCWebApp.Models.Enums;

namespace UrbanWatchMVCWebApp.Models.DataModels;
public class Route
{
    public int Id { get; set; }
    public string? RouteId { get; set; }
    public string? AgencyId { get; set; }
    public string? RouteShortName { get; set; }
    public string? RouteLongName { get; set; }
    public string? RouteColor { get; set; }
    public RouteType? RouteType { get; set; }
}
