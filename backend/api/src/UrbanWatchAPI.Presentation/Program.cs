using UrbanWatchAPI.Infrastructure.Mongo;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5020");

// Bind env vars to config class
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("Mongo"));

// Register MongoContext as a singleton
builder.Services.AddSingleton<MongoContext>();

// Add custom services to DI Container
builder.Services.AddSingleton<VehicleSnapshotRepository>();

builder.Services.AddControllers();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();