using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using UrlShortener.Storage.Dao.Abstraction;

namespace UrlShortener.Logic.Queries.Handlers
{
    public class GetRedirectPathHandler : IRequestHandler<GetRedirectPath, Result<string>>
    {
        private readonly IUrlDao _urlDao;

        public GetRedirectPathHandler(IUrlDao urlDao)
        {
            _urlDao = urlDao;
        }
        public Task<Result<string>> Handle(
            GetRedirectPath request,
             CancellationToken cancellationToken) =>
             _urlDao
                .GetByPath(request.ShortPath)
                .OnSuccess(url => url.Path);
    }
}