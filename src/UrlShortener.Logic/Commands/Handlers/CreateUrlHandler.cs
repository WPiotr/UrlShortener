using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using UrlShortener.Logic.Commands;
using UrlShortener.Logic.Validators.Abstraction;
using UrlShortener.Storage.Dao.Abstraction;
using UrlShortener.Storage.Models;

namespace UrlShortener.Logic.Commands.Handlers
{
    public class CreateUrlHandler : IRequestHandler<CreateUrl, Result<string>>
    {
        private readonly IUrlDao _urlDao;
        private readonly ICreateUrlValidator _createUrlValidator;
        public CreateUrlHandler(
            IUrlDao urlDao,
            ICreateUrlValidator createUrlValidator)
        {
            _urlDao = urlDao;
            _createUrlValidator = createUrlValidator;
        }

        public Task<Result<string>> Handle(
            CreateUrl request, CancellationToken cancellationToken)
        {
            var url = new Url(request.Id, request.Path);
            return _createUrlValidator
                .Validate(request)
                .OnSuccess( command => _urlDao
                    .Save(url)
                    .OnSuccess(() => url.ShortPath), false);
        }

    }
}