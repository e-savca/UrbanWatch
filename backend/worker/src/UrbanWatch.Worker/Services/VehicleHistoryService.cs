using MongoDB.Driver;
using UrbanWatch.Worker.Infrastructure.Data;
using UrbanWatch.Worker.Models;

namespace UrbanWatch.Worker.Services;

public class VehicleHistoryService
{
    private readonly IMongoCollection<Vehicle> _collection;

    public VehicleHistoryService(MongoContext mongoContext)
    {
        _collection = mongoContext.VehicleHistory;
    }

    public async Task SaveBatchAsync(List<Vehicle> vehicles, CancellationToken ct = default)
    {
        if (vehicles?.Any() != true) return;

        await _collection.InsertManyAsync(vehicles, cancellationToken: ct);
    }
    
    public async Task<List<Vehicle>> GetLastAsync(int count = 5, CancellationToken ct = default)
    {
        return await _collection
            .Find(_ => true)
            .SortByDescending(v => v.Timestamp)
            .Limit(count)
            .ToListAsync(ct);
    }

}
