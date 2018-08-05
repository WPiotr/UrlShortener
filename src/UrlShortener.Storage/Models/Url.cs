using System;

namespace UrlShortener.Storage.Models
{
    public class Url
    {
        public Url(string path, Guid id)
        {
            Id = id;
            Path = path;
        }
        public Guid Id { get; }
        public string Path { get; }
        public string ShortPath => Id.ToString("N");
    }
}