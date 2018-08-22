using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Models;
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
        public Task<IActionResult> Post([FromBody] UrlViewModel model) =>
            _mediator
                .Send(new CreateUrl(model.Url))
                .OnBoth(res => res.IsSuccess
                    ? Ok(new { res.Value }) as IActionResult
                    : BadRequest(res.Error) as IActionResult);
    }
}