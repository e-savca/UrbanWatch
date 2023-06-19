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
        public bool AreVehiclesDuplicates(Vehicle[] NewData)
        {
            List<string> oldDataStrings = Vehicles.Select(item => item.ToString()).ToList();
            List<string> newDataStrings = NewData.Select(item => item.ToString()).ToList();

            return oldDataStrings.SequenceEqual(newDataStrings);
        }

    }
}
