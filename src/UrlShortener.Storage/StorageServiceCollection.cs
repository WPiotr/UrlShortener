using System;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Storage.Dao;
using UrlShortener.Storage.Dao.Abstraction;

namespace UrlShortener.Storage
{
    public static class StorageServiceCollection
    {
          public static IServiceCollection RegisterStorage(this IServiceCollection services) =>
            services
                .AddSingleton<IUrlDao, InMemoryUrlDao>();
    }
}