using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrbanWatch.Worker.Models;

namespace UrbanWatch.Worker.Infrastructure.Data;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoSettings> mongoSettings)
    {
        var settings = mongoSettings.Value;

        var client = new MongoClient($"mongodb://{settings.Username}:{settings.Password}@{settings.Host}:{settings.Port}");
        _database = client.GetDatabase(settings.Database);
    }
    public IMongoCollection<Vehicle> VehicleHistory =>
        _database.GetCollection<Vehicle>("vehicle_history");


}