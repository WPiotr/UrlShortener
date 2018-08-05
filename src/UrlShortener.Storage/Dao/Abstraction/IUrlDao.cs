using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using UrlShortener.Storage.Models;

namespace UrlShortener.Storage.Dao.Abstraction
{
    public interface IUrlDao
    {
        Task<Result> Save(Url url);
        Task<Result<Url>> GetById(Guid id);
        Task<Result<Url>> GetByPath(string path);
    }
}