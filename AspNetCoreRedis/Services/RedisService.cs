using StackExchange.Redis;
using System.Text.Json;

namespace AspNetCoreRedis.Services
{
    public class RedisService
    {
        private ConnectionMultiplexer _redis;

        public IDatabase Database { get; set; }
        public RedisService(IConfiguration configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration.GetValue<string>("RedisOption:Configuration"));
            Database = _redis.GetDatabase(configuration.GetValue<int>("RedisOption:DefaultDatabase"));
        }

        public IDatabase GetDatabase(int db)
        {
            return _redis.GetDatabase(db);
        }
    }
}
