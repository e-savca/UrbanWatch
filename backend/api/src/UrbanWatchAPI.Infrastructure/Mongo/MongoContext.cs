using MongoDB.Driver;
using Microsoft.Extensions.Options;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;

namespace UrbanWatchAPI.Infrastructure.Mongo;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoSettings> mongoSettings)
    {
        var settings = mongoSettings.Value;

        var client = new MongoClient($"mongodb://{settings.Username}:{settings.Password}@{settings.Host}:{settings.Port}");
        _database = client.GetDatabase(settings.Database);
    }
    public IMongoCollection<VehicleSnapshotDocument> VehicleHistory =>
        _database.GetCollection<VehicleSnapshotDocument>("vehicle_history");
}