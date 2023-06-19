namespace UrbanWatchMVCWebApp.Services
{
    public interface IDataIntegrationService
    {        
        public Task UpdateData();
        public Task InitializeData();
    }
}
