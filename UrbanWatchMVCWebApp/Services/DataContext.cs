using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services;

public class DataContext
{
    public List<Models.Route> Routes { get; set; } = null!;
    public List<Shape> Shapes { get; set; } = null!;
    public List<Stop> Stops { get; set; } = null!;
    public List<StopTimes> StopTimes { get; set; } = null!;
    public List<Trip> Trips { get; set; } = null!;
    public List<Vehicle> Vehicles { get; set; } = null!;
    public async Task<bool> AreVehiclesDuplicatesAsync(List<Vehicle> NewData)
    {
        List<string> oldDataStrings = await Vehicles.Select(item => item.ToString()).AsQueryable().ToListAsync();
        List<string> newDataStrings = await NewData.Select(item => item.ToString()).AsQueryable().ToListAsync();

        return oldDataStrings.SequenceEqual(newDataStrings);
    }

}
