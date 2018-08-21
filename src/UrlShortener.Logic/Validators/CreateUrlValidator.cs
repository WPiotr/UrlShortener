using System;
using CSharpFunctionalExtensions;
using UrlShortener.Logic.Commands;
using UrlShortener.Logic.Validators.Abstraction;
using Valit;

namespace UrlShortener.Logic.Validators
{
    public class CreateUrlValidator : ICreateUrlValidator
    {
        public Result<CreateUrl> Validate(CreateUrl commandFixture)
        {
            var validationResult = Validator.Validate(commandFixture);
            
            return validationResult.Succeeded 
                ? Result.Ok(commandFixture)
                : Result.Fail<CreateUrl>(string.Join(Environment.NewLine, validationResult.ErrorMessages));
        }

        private IValitator<CreateUrl> Validator =>
            ValitRules<CreateUrl>
                .Create()
                .Ensure(m => m.Path, _ => _
                        .Required()
                        .Matches("(https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|www\\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|https?:\\/\\/(?:www\\.|(?!www))[a-zA-Z0-9]\\.[^\\s]{2,}|www\\.[a-zA-Z0-9]\\.[^\\s]{2,})"))
                .CreateValitator();

    }
}