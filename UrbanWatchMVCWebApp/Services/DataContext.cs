using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.Models.UiModels;

namespace UrbanWatchMVCWebApp.Services;

public class DataContext
{
    public IQueryable<Models.UiModels.Route> Routes { get; set; } = null!;
    public IQueryable<Shape> Shapes { get; set; } = null!;
    public IQueryable<Stop> Stops { get; set; } = null!;
    public IQueryable<StopTimes> StopTimes { get; set; } = null!;
    public IQueryable<Trip> Trips { get; set; } = null!;
    public IQueryable<Vehicle> Vehicles { get; set; } = null!;
    public async Task<bool> AreVehiclesDuplicatesAsync(IQueryable<Vehicle> NewData)
    {
        IEnumerable<string> oldDataStrings = Vehicles.Select(item => item.ToString());
        IEnumerable<string> newDataStrings = NewData.Select(item => item.ToString());

        return oldDataStrings.SequenceEqual(newDataStrings);
    }

}
