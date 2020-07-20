using ECommerce.Ordering.Api.Application.Commands;
using ECommerce.Ordering.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [HttpGet]
        [ProducesResponseType(typeof(GetBuyersQueryResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var query = new GetBuyersQuery();
            var buyers = await _mediator.Send(query);

            if (!buyers.Any())
                return NotFound();

            return Ok(buyers);
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