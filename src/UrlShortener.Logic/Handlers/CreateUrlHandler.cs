using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using UrlShortener.Logic.Commands;
using UrlShortener.Storage.Dao.Abstraction;
using UrlShortener.Storage.Models;

namespace UrlShortener.Logic.Handlers
{
    public class CreateUrlHandler : IRequestHandler<CreateUrl, Result<string>>
    {
        private readonly IUrlDao _urlDao;
        public CreateUrlHandler(IUrlDao urlDao)
        {
            _urlDao = urlDao;
        }

        public Task<Result<string>> Handle(
            CreateUrl request, CancellationToken cancellationToken)
        {
            var url = new Url(request.Id, request.Path);
            return _urlDao
                .Save(url)
                .OnSuccess(() => url.ShortPath);
        }

    }
}