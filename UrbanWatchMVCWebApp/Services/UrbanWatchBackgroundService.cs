using UrbanWatchMVCWebApp.Services.Interfaces;

namespace UrbanWatchMVCWebApp.Services
{
    public class UrbanWatchBackgroundService : BackgroundService
    {
        private readonly IDataIntegrationService _dataIntegrationService;
        private readonly ILogger<UrbanWatchBackgroundService> _logger;

        public UrbanWatchBackgroundService(IDataIntegrationService dataIntegrationService, ILogger<UrbanWatchBackgroundService> logger)
        {
            _dataIntegrationService = dataIntegrationService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("UrbanWatchBackgroundService is running.");

            await _dataIntegrationService.InitializeDataAsync();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                   await _dataIntegrationService.UpdateDataAsync();
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError("UrbanWatchBackgroundService is stopping.");
                _logger.LogError($"OperationCanceledException ex.Message:\n{ex.Message}.");
            }
        }
    }
}
