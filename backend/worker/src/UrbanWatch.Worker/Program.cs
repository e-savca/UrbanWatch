using UrbanWatch.Worker;
using UrbanWatch.Worker.Clients;
using UrbanWatch.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddHostedService<VehicleWorker>();

// register dependencies 
builder.Services.AddSingleton<TranzyClient>();
builder.Services.AddSingleton<ApiKeyManager>();

var host = builder.Build();
host.Run();
