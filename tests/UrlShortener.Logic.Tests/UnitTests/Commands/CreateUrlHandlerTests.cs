using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CSharpFunctionalExtensions;
using FakeItEasy;
using FluentAssertions;
using UrlShortener.Logic.Commands;
using UrlShortener.Logic.Commands.Handlers;
using UrlShortener.Storage.Dao.Abstraction;
using UrlShortener.Storage.Models;
using UrlShortener.Tests.Abstraction;
using UrlShortener.Logic.Validators.Abstraction;
using Xunit;
using AutoFixture.Xunit2;

namespace UrlShortener.Logic.Tests.UnitTests.Commands
{
    public class CreateUrlHandlerTests
    {
        [Theory]
        [AutoData]
        public async Task Handle_DaoReturnsOk_ReturnOk(CreateUrl commandFixture)
        {
            var urlDaoMock = A.Fake<IUrlDao>();

            A.CallTo(() => urlDaoMock
                .Save(A<Url>.That
                    .Matches(url => url.Path.Equals(commandFixture.Path)
                        && url.Id.Equals(commandFixture.Id))))
                .Returns(Task.FromResult(Result.Ok()));
            var createUrlValidatorMock = A.Fake<ICreateUrlValidator>();
            A.CallTo(() => createUrlValidatorMock
                .Validate(commandFixture))
                .Returns(Result.Ok(commandFixture));

            var sut = new CreateUrlHandler(urlDaoMock, createUrlValidatorMock);

            var result = await sut.Handle(commandFixture, CancellationToken.None);
            result.IsSuccess.Should().BeTrue();
            string expectedValue = commandFixture.Id.ToString("N");
            result.Value.Should().Be(expectedValue);
        }

        [Theory]
        [AutoData]
        public async Task Handle_DaoReturnsFailure_ReturnsFailure(CreateUrl commandFixture, string errorMessageFixture)
        {
            var urlDaoMock = A.Fake<IUrlDao>();

            A.CallTo(() => urlDaoMock
                .Save(A<Url>.That
                    .Matches(url => url.Path.Equals(commandFixture.Path)
                        && url.Id.Equals(commandFixture.Id))))
                .Returns(Task.FromResult(Result.Fail(errorMessageFixture)));
            var createUrlValidatorMock = A.Fake<ICreateUrlValidator>();
            A.CallTo(() => createUrlValidatorMock
                .Validate(commandFixture))
                .Returns(Result.Ok(commandFixture));
            var sut = new CreateUrlHandler(urlDaoMock, createUrlValidatorMock);

            var result = await sut.Handle(commandFixture, CancellationToken.None);
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(errorMessageFixture);
        }

        [Theory]
        [AutoData]
        public async Task Handle_ValidatorReturnsFailure_ReturnsFailure(CreateUrl commandFixture, string errorMessageFixture)
        {
            var urlDaoMock = A.Fake<IUrlDao>();

            var createUrlValidatorMock = A.Fake<ICreateUrlValidator>();
            A.CallTo(() => createUrlValidatorMock
                .Validate(commandFixture))
                .Returns(Result.Fail<CreateUrl>(errorMessageFixture));
            var sut = new CreateUrlHandler(urlDaoMock, createUrlValidatorMock);

            var result = await sut.Handle(commandFixture, CancellationToken.None);
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(errorMessageFixture);
        }
    }
}