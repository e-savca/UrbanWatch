using StackExchange.Redis;

namespace UrbanWatch.Worker.Infrastructure.Data;

public class RedisContext(string connectionString)
{
    private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect(connectionString);
    public IDatabase Db => _redis.GetDatabase();
}