using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
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
    public class GetRedirectPathHandlerTests : BaseUnitTest
    {
        [Fact]
        public async Task Handle_DaoReturnsPath_ReturnPath()
        {
            //Given
            var shortPathFixture = Fixture.Create<string>();
            var queryFixture = new GetRedirectPath(shortPathFixture);

            var urlDaoMock = A.Fake<IUrlDao>();
            var urlFixture = Fixture.Create<Url>();
            A.CallTo(
                () => urlDaoMock
                .GetByPath(A<string>.That.IsEqualTo(shortPathFixture)))
                .Returns(Task.FromResult(Result.Ok(urlFixture)));

            var sut = new GetRedirectPathHandler(urlDaoMock);
            //When
            var result = await sut.Handle(queryFixture, CancellationToken.None);
            //Then
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(urlFixture.Path);
        }

        [Fact]
        public async Task Handle_DaoReturnsFailure_ReturnFailure()
        {
            //Given
            var shortPathFixture = Fixture.Create<string>();
            var queryFixture = new GetRedirectPath(shortPathFixture);

            var urlDaoMock = A.Fake<IUrlDao>();
            var errorMessage = Fixture.Create<string>();
            A.CallTo(
                () => urlDaoMock
                .GetByPath(A<string>.That.IsEqualTo(shortPathFixture)))
                .Returns(Task.FromResult(Result.Fail<Url>(errorMessage)));

            var sut = new GetRedirectPathHandler(urlDaoMock);
            //When
            var result = await sut.Handle(queryFixture, CancellationToken.None);
            //Then
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(errorMessage);
        }
    }
}