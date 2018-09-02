using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using UrlShortener.Storage.Connectivity.Abstraction;
using UrlShortener.Storage.Dao.Abstraction;
using UrlShortener.Storage.Models;

namespace UrlShortener.Storage.Dao
{
    public class RedisUrlDao : IUrlDao
    {
        private readonly IRedisConnector _redisConnector;

        public RedisUrlDao(IRedisConnector redisConnector)
        {
            _redisConnector = redisConnector;
        }

        public async Task<Result> Save(Url url)
        {
            try
            {
                var db = _redisConnector.GetDatabase();
                var serializedUrl = JsonConvert.SerializeObject(url);
                var result = await db.StringSetAsync(url.Id.ToString("N"), serializedUrl, TimeSpan.FromDays(30));
                return result ? Result.Ok() : Result.Fail("");
            }
            catch (Exception e)
            {
                return Result.Fail(e.ToString());
            }
        }

        public async Task<Result<Url>> GetById(Guid id)
        {
            try
            {
                var db = _redisConnector.GetDatabase();
                var result = await db.StringGetAsync(id.ToString("N"));
                var value = JsonConvert.DeserializeObject<Url>(result);
                return Result.Ok(value);
            }
            catch (Exception e)
            {
                return Result.Fail<Url>(e.ToString());
            }
        }

        public async Task<Result<Url>> GetByShortPath(string shortPath)
        {
            try
            {
                var db = _redisConnector.GetDatabase();
                var result = await db.StringGetAsync(shortPath);
                var value = JsonConvert.DeserializeObject<Url>(result);
                return Result.Ok(value);
            }
            catch (Exception e)
            {
                return Result.Fail<Url>(e.ToString());
            }
        }
    }
}