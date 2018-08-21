using CSharpFunctionalExtensions;
using UrlShortener.Logic.Commands;

namespace UrlShortener.Logic.Validators.Abstraction
{
    public interface ICreateUrlValidator
    {
        Result Validate(CreateUrl commandFixture);
    }
}