using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Logic.Commands;

namespace UrlShortener.Api.Controllers
{
    [Route("api/[controller]")]
    public class UrlController : Controller
    {
        private readonly IMediator _mediator;
        public UrlController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<IActionResult> Post([FromBody] string url) =>
            _mediator
                .Send(new CreateUrl(url))
                .OnBoth(res => res.IsSuccess 
                    ? Ok() as IActionResult 
                    : BadRequest(res.Error) as IActionResult);
    }
}