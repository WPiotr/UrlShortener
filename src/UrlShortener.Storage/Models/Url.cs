using System;

namespace UrlShortener.Storage.Models
{
    public class Url
    {
        public Url(Guid id, string path)
        {
            Id = id;
            Path = path;
        }
        public Guid Id { get; }
        public string Path { get; }
        public string ShortPath => Id.ToString("N");
    }
}