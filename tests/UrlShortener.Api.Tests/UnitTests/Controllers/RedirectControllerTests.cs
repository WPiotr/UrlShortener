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
        public async Task GoToPath_MediatorReturnsOkWithUrl_ReturnRedirectResultToPath()
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

        [Fact]
        public async Task GoToPath_MediatorReturnsError_ReturnRedirectResultToBadPage()
        {
            var mediatorMock = A.Fake<IMediator>();
            var shortPathFixture = Fixture.Create<string>();
            var errorMessageFixture = Fixture.Create<string>();
            A.CallTo(
                () => mediatorMock
                    .Send(A<GetRedirectPath>.That
                        .Matches(query => query.ShortPath == shortPathFixture)
                        , CancellationToken.None))
                .Returns(Task.FromResult(Result.Fail<string>(errorMessageFixture)));
            var sut = new RedirectController(mediatorMock);
            var result = await sut.GoToPath(shortPathFixture);
            result.Should().BeAssignableTo<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("BadPage");
        }
    }
}
