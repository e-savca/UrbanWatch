using UrbanWatchMVCWebApp.IServices;
using UrbanWatchMVCWebApp.Models.UiModels;

namespace UrbanWatchMVCWebApp.Services;

public abstract class DataSnapshot
{
    public abstract IQueryable<Models.UiModels.Vehicle> Vehicles { get; set; }

    public virtual async Task<bool> AreVehiclesDuplicatesAsync(IQueryable<Vehicle> newData)
    {
        IEnumerable<string> oldDataStrings = Vehicles.Select(item => item.ToString());
        IEnumerable<string> newDataStrings = newData.Select(item => item.ToString());

        return oldDataStrings.SequenceEqual(newDataStrings);
    }
}