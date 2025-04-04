using UrbanWatch.Worker;
using UrbanWatch.Worker.Clients;
using UrbanWatch.Worker.ConfigManager;
using UrbanWatch.Worker.Infrastructure.Data;
using UrbanWatch.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddHostedService<VehicleWorker>();

// bind env vars
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("Mongo"));

// Register dependencies
builder.Services.AddSingleton<MongoContext>();
// builder.Services.AddSingleton(new RedisContext("redis:6379"));

builder.Services.AddSingleton<TranzyClient>();
builder.Services.AddSingleton<EnvManager>();

builder.Services.AddSingleton<VehicleHistoryService>();

var host = builder.Build();
host.Run();
