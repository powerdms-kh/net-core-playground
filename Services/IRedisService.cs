using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace TodoApi.Services
{
    public interface IRedisService
    {
        ConnectionMultiplexer GetConnection();
    }

    public class RedisService : IRedisService
    {
        private ConnectionMultiplexer Connection;
        public RedisService(IConfiguration config)
        {
            this.Connection = ConnectionMultiplexer.Connect(config["RedisHost"]);
        }

        public ConnectionMultiplexer GetConnection()
        {
            return this.Connection;
        }

        public IDatabase GetDatabase()
        {
            return this.Connection.GetDatabase();
        }
    }
}
