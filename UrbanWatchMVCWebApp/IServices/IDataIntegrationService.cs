namespace UrbanWatchMVCWebApp.IServices
{
    public interface IDataIntegrationService
    {
        public Task UpdateDataAsync();
        public Task InitializeDataAsync();
    }
}
