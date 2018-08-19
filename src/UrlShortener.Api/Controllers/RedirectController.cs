using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Models;
using UrlShortener.Logic.Queries;

namespace UrlShortener.Api.Controllers
{
    [Route("")]
    public class RedirectController : Controller
    {
        private readonly IMediator _mediator;

        public RedirectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{shortPath}")]
        public  Task<IActionResult> GoToPath( string shortPath) =>
            _mediator.Send(new GetRedirectPath(shortPath))
            .OnBoth(result => 
                result.IsSuccess 
                ? Redirect(result.Value) as IActionResult
                : View("BadPage", new BadPageViewModel(shortPath, "eee"))); 
    }
}