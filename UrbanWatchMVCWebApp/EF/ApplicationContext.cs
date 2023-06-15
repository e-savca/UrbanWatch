using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<StopTimes> StopTimes { get; set; } = null!;
        public DbSet<Stop> Stops { get; set; } = null!;
        public DbSet<Shape> Shapes { get; set; } = null!;
        public DbSet<Models.Route> Routes { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
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
    }
}
