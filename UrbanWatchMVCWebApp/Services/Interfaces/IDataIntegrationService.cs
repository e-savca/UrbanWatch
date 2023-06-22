namespace UrbanWatchMVCWebApp.Services.Interfaces
{
    public interface IDataIntegrationService
    {
        public Task UpdateData();
        public Task InitializeData();
    }
}
