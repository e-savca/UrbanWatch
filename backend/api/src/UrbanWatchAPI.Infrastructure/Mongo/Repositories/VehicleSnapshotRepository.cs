using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using UrbanWatchAPI.Domain.Entities.PublicTransportEntities;
using UrbanWatchAPI.Infrastructure.Mongo.Documents;
using UrbanWatchAPI.Infrastructure.Mongo.Mappers;

namespace UrbanWatchAPI.Infrastructure.Mongo.Repositories;

public class VehicleSnapshotRepository
{
    private readonly IMongoCollection<VehicleSnapshotDocument> _collection;
    private readonly ILogger<VehicleSnapshotRepository> _logger;

    public VehicleSnapshotRepository(MongoContext mongoContext)
    {
        _collection = mongoContext.VehicleHistory;
    }
    
    public async Task<List<VehicleSnapshot>> GetRecorsForLastAsync(int seconds)
    {
        var thresholdTime = DateTime.UtcNow.AddSeconds(-seconds);

        var filter = Builders<VehicleSnapshotDocument>.Filter.Gte(x => x.Timestamp, thresholdTime);

        var documents = await _collection.Find(filter).ToListAsync();

        return new VehicleSnapshotMapper().ToDomain(documents);
    }

    public async Task<VehicleSnapshot?> GetLastAsync()
    {
        var filter = Builders<VehicleSnapshotDocument>.Filter.Gte(x => x.Timestamp, DateTime.UtcNow);
        
        var document = await _collection.Find(filter).FirstOrDefaultAsync();
        return new VehicleSnapshotMapper().ToDomain(document);
    }

}