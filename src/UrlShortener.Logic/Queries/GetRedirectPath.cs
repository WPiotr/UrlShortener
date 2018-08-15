using CSharpFunctionalExtensions;
using MediatR;

namespace UrlShortener.Logic.Queries
{
    public class GetRedirectPath : IRequest<Result<string>>
    {
        public string ShortPath { get; }

        public GetRedirectPath(string shortPath)
        {
            ShortPath = shortPath;
        }
    }
}