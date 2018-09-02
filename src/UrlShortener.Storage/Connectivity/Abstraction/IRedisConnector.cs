using StackExchange.Redis;

namespace UrlShortener.Storage.Connectivity.Abstraction
{
    public interface IRedisConnector
    {
        IDatabase GetDatabase();
    }
}