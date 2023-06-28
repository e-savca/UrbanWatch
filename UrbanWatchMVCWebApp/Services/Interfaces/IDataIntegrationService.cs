namespace UrbanWatchMVCWebApp.Services.Interfaces
{
    public interface IDataIntegrationService
    {
        public Task UpdateDataAsync();
        public Task InitializeDataAsync();
    }
}
