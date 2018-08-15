using System;
using AutoFixture;
using FakeItEasy;
using MediatR;
using UrlShortener.Api.Controllers;
using UrlShortener.Logic.Queries;
using FluentAssertions;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;
using System.Threading;
using UrlShortener.Api.Tests;

namespace UrlShortener.UnitTests.Controllers
{
    public class RedirectControllerTests : BaseUnitTest
    {
        [Fact]
        public async Task GoToPath_MediatorReturnsOkWithUrl_ReturnRedirectResult()
        {
            var mediatorMock = A.Fake<IMediator>();
            var shortPathFixture = Fixture.Create<string>();
            var pathFixture = Fixture.Create<Uri>().ToString();
            A.CallTo(
                () => mediatorMock
                    .Send(A<GetRedirectPath>.That
                        .Matches(query => query.ShortPath == shortPathFixture)
                        , CancellationToken.None))
                .Returns(Task.FromResult(Result.Ok(pathFixture)));
            var sut = new RedirectController(mediatorMock);
            var result = await sut.GoToPath(shortPathFixture);
            result.Should().BeAssignableTo<RedirectResult>();
            result.As<RedirectResult>().Url.Should().Be(pathFixture);
        }
    }
}
