using Microsoft.Extensions.Configuration;

namespace UrlShortener.Storage.Configuration
{
    public static class StorageConfiguration
    {
        public static string RedisServer(this IConfiguration configuration) => 
            configuration.GetSection(nameof(RedisServer))["Url"];
        
    }
}