using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class DataContext
    {
        public Models.Route[] Routes { get; set; } = null!;
        public Shape[] Shapes { get; set; } = null!;
        public Stop[] Stops { get; set; } = null!;
        public StopTimes[] StopTimes { get; set; } = null!;
        public Trip[] Trips { get; set; } = null!;
        public Vehicle[] Vehicles { get; set; } = null!;
    }
}
