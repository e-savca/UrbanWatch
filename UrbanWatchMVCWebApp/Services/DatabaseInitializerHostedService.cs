using System.Threading;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class DatabaseInitializerHostedService : IHostedService
    {
        private readonly ILogger<DatabaseInitializerHostedService> _logger;
        private readonly ITranzyService _tranzyService;
        private readonly IServiceProvider _serviceProvider;
        private int executionCount = 0;
        private Timer? _timer = null;
        public DatabaseInitializerHostedService(ITranzyService tranzyService, IServiceProvider serviceProvider, ILogger<DatabaseInitializerHostedService> logger)
        {
            _tranzyService = tranzyService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            await InitializeDatabase();
            _timer = new Timer(async (state) => await UpdateDatabase(state), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public async Task UpdateDatabase(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation($"Timed Hosted Service is working. Count: {count}");

            using (var scope = _serviceProvider.CreateScope())
            {
                ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    try
                    {
                        var vehicles = await _tranzyService.GetVehiclesDataAsync();

                        _context.Vehicles.AddRange(vehicles);

                        await _context.SaveChangesAsync();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
            }
            
        }
        public async Task InitializeDatabase()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                ApplicationContext _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    try
                    {
                        _ = await _context.Database.EnsureDeletedAsync();
                        _ = await _context.Database.EnsureCreatedAsync();

                        var vehicles = await _tranzyService.GetVehiclesDataAsync();
                        var routes = await _tranzyService.GetRoutesDataAsync();
                        var trips = await _tranzyService.GetTripsDataAsync();
                        var shapes = await _tranzyService.GetShapesDataAsync();
                        var stops = await _tranzyService.GetStopsDataAsync();
                        var stopTimes = await _tranzyService.GetStopTimesDataAsync();

                        _context.Vehicles.AddRange(vehicles);
                        _context.Routes.UpdateRange(routes);
                        _context.Trips.UpdateRange(trips);
                        _context.Shapes.UpdateRange(shapes);
                        _context.Stops.UpdateRange(stops);
                        _context.StopTimes.UpdateRange(stopTimes);

                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        throw;
                    }
            }
            
        }
    }
}
