using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using CSharpFunctionalExtensions;
using FakeItEasy;
using FluentAssertions;
using UrlShortener.Logic.Queries;
using UrlShortener.Logic.Queries.Handlers;
using UrlShortener.Storage.Dao.Abstraction;
using UrlShortener.Storage.Models;
using UrlShortener.Tests.Abstraction;
using Xunit;

namespace UrlShortener.Logic.Tests.UnitTests.Queries
{
    public class GetRedirectPathHandlerTests
    {
        [Theory]
        [AutoData]
        public async Task Handle_DaoReturnsPath_ReturnPath(GetRedirectPath queryFixture, Url urlFixture)
        {
            var urlDaoMock = A.Fake<IUrlDao>();
            A.CallTo(
                () => urlDaoMock
                .GetByShortPath(A<string>.That.IsEqualTo(queryFixture.ShortPath)))
                .Returns(Task.FromResult(Result.Ok(urlFixture)));

            var sut = new GetRedirectPathHandler(urlDaoMock);

            var result = await sut.Handle(queryFixture, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(urlFixture.Path);
        }

        [Theory]
        [AutoData]
        public async Task Handle_DaoReturnsFailure_ReturnFailure(GetRedirectPath queryFixture, string errorMessageFixture)
        {
            var urlDaoMock = A.Fake<IUrlDao>();
            A.CallTo(
                () => urlDaoMock
                .GetByShortPath(A<string>.That.IsEqualTo(queryFixture.ShortPath)))
                .Returns(Task.FromResult(Result.Fail<Url>(errorMessageFixture)));

            var sut = new GetRedirectPathHandler(urlDaoMock);

            var result = await sut.Handle(queryFixture, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(errorMessageFixture);
        }
    }
}