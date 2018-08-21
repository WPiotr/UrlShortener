using FluentAssertions;
using UrlShortener.Logic.Commands;
using UrlShortener.Logic.Validators;
using Xunit;

namespace UrlShortener.Logic.Tests.UnitTests.Validators
{
    public class CreateUrlValidatorTests
    {
        [Theory]
        [InlineData("www.a.com")]
        [InlineData("https://www.a.com")]
        [InlineData("http://www.a.com")]
        public void Validate_CorrectUrlPath_ReturnsOk(string path)
        {
            var commandFixture = new CreateUrl(path);
            var sut = new CreateUrlValidator();
            var result = sut.Validate(commandFixture);   
            result.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("a.com")]
        [InlineData("test.com")]
        public void Validate_InCorrectUrlPath_ReturnsFail(string path)
        {
            var commandFixture = new CreateUrl(path);
            var sut = new CreateUrlValidator();
            var result = sut.Validate(commandFixture);   
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNullOrEmpty();
        }
    }
}