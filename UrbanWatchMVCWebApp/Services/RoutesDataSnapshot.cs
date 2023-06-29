using UrbanWatchMVCWebApp.Models.UiModels;

namespace UrbanWatchMVCWebApp.Services;

public class RoutesDataSnapshot : DataSnapshot
{
    public override IQueryable<Models.UiModels.Vehicle> Vehicles { get; set; } = null!;
}

