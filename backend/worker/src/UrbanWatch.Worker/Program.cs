using UrbanWatch.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddHostedService<VehicleWorker>();

var host = builder.Build();
host.Run();
