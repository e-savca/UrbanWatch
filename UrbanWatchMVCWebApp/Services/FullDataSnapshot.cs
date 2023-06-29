using UrbanWatchMVCWebApp.Models.UiModels;

namespace UrbanWatchMVCWebApp.Services;

public class FullDataSnapshot : DataSnapshot
{
    public IQueryable<Models.UiModels.Route> Routes { get; set; } = null!;
    public IQueryable<Shape> Shapes { get; set; } = null!;
    public IQueryable<Stop> Stops { get; set; } = null!;
    public IQueryable<StopTimes> StopTimes { get; set; } = null!;
    public IQueryable<Trip> Trips { get; set; } = null!;
    public override IQueryable<Vehicle> Vehicles { get; set; } = null!;
}
