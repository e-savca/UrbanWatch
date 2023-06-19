using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public interface IDataProviderService
    {
        public Task<Vehicle[]> GetVehiclesDataAsync();
        public Task<Models.Route[]> GetRoutesDataAsync();
        public Task<Trip[]> GetTripsDataAsync();
        public Task<Shape[]> GetShapesDataAsync();
        public Task<Stop[]> GetStopsDataAsync();
        public Task<StopTimes[]> GetStopTimesDataAsync();
    }
}
