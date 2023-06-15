using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.EF
{
    public class ApplicationContext : DbContext
    {
        private readonly ITranzyService _tranzyService;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<StopTimes> StopTimes { get; set; } = null!;
        public DbSet<Stop> Stops { get; set; } = null!;
        public DbSet<Shape> Shapes { get; set; } = null!;
        public DbSet<Models.Route> Routes { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, [FromServices] ITranzyService tranzyService) : base(options)
        {
            _tranzyService = tranzyService;
        }
        public async Task UpdateVehiclesData()
        {
            using (var scope = this.Database.BeginTransaction())
            {
                try
                {
                    var vehicles = await _tranzyService.GetVehiclesDataAsync();

                    Vehicles.AddRange(vehicles);

                    await SaveChangesAsync();

                    await scope.CommitAsync();
                }
                catch (Exception)
                {
                    await scope.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task UpdateDatabase()
        {
            using (var scope = this.Database.BeginTransaction())
            {
                try
                {
                    var vehicles = await _tranzyService.GetVehiclesDataAsync();
                    var routes = await _tranzyService.GetRoutesDataAsync();
                    var trips = await _tranzyService.GetTripsDataAsync();
                    var shapes = await _tranzyService.GetShapesDataAsync();
                    var stops = await _tranzyService.GetStopsDataAsync();
                    var stopTimes = await _tranzyService.GetStopTimesDataAsync();

                    Vehicles.AddRange(vehicles);
                    Routes.AddRange(routes);
                    Trips.AddRange(trips);
                    Shapes.AddRange(shapes);
                    Stops.AddRange(stops);
                    StopTimes.AddRange(stopTimes);

                    await SaveChangesAsync();

                    await scope.CommitAsync();
                }
                catch (Exception ex)
                {
                    await scope.RollbackAsync();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    throw;
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Route>().HasKey(k => k.Id);
            modelBuilder.Entity<Shape>().HasKey(k => k.Id);
            modelBuilder.Entity<Stop>().HasKey(k => k.Id);
            modelBuilder.Entity<StopTimes>().HasKey(k => k.Id);
            modelBuilder.Entity<Trip>().HasKey(k => k.Id);
            modelBuilder.Entity<Vehicle>().HasKey(k => k.Id);

            base.OnModelCreating(modelBuilder);
        }
        public async Task OnApplicationStarted()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }


    }
}
