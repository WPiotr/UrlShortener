using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CSharpFunctionalExtensions;
using UrlShortener.Storage.Models;
using UrlShortener.Storage.Dao.Abstraction;

namespace UrlShortener.Storage.Dao
{
    public class InMemoryUrlDao : IUrlDao
    {
        private readonly IList<Url> _urls;
        public InMemoryUrlDao()
        {
            _urls = new List<Url>();
        }

        public Task<Result> Save(Url url)
        {
            try
            {
                _urls.Add(url);
                return Task.FromResult(Result.Ok());
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result.Fail(ex.ToString()));
            }
        }

        public Task<Result<Url>> GetById(Guid id)
        {
            try
            {
                var url = _urls.FirstOrDefault(u => u.Id == id);
                if (url != null)
                {
                    Task.FromResult(Result.Ok(url));
                }
                return Task.FromResult(Result.Fail<Url>("Url not found"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result.Fail<Url>(ex.ToString()));
            }
        }

        public Task<Result<Url>> GetByPath(string path)
        {
            try
            {
                var url = _urls.FirstOrDefault(u => u.Path.Equals(path));
                if (url != null)
                {
                    Task.FromResult(Result.Ok(url));
                }
                return Task.FromResult(Result.Fail<Url>("Url not found"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result.Fail<Url>(ex.ToString()));
            }
        }
    }
}