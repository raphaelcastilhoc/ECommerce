using ECommerce.Ordering.Api.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BuyersController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(AddBuyerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}