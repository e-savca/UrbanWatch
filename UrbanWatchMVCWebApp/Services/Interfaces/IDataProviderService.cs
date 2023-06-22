using UrbanWatchMVCWebApp.Models.ApiModels.TranzyV1Models;

namespace UrbanWatchMVCWebApp.Services.Interfaces
{
    public interface IDataProviderService
    {
        public Task<List<Vehicle>> GetVehiclesDataAsync();
        public Task<List<Models.ApiModels.TranzyV1Models.Route>> GetRoutesDataAsync();
        public Task<List<Trip>> GetTripsDataAsync();
        public Task<List<Shape>> GetShapesDataAsync();
        public Task<List<Stop>> GetStopsDataAsync();
        public Task<List<StopTimes>> GetStopTimesDataAsync();
    }
}
