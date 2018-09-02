using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using UrlShortener.Storage.Configuration;
using UrlShortener.Storage.Connectivity.Abstraction;

namespace UrlShortener.Storage.Connectivity
{
    public class RedisConnector : IRedisConnector
    {

        private readonly IConfiguration _configuration;

        public RedisConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDatabase GetDatabase()
        {
            var redis = ConnectionMultiplexer.Connect(_configuration.RedisServer());
            return redis.GetDatabase(0);
        }
    }
}