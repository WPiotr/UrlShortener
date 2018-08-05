using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using UrlShortener.Logic.Commands;

namespace UrlShortener.Logic.Handlers
{
    public class CreateUrlHandler : IRequestHandler<CreateUrl, Result>
    {
        public Task<Result> Handle(CreateUrl request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}