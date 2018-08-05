using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace UrlShortener.Logic.Commands
{
    public class CreateUrl : IRequest<Result>
    {

        public CreateUrl(string path)
        {
            Id = Guid.NewGuid();
            Path = path;

        }
        public Guid Id { get; set; }
        public string Path { get; }
    }
}