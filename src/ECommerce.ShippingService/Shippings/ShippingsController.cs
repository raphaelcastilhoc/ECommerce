using ECommerce.ShippingService.Shippings.Commands;
using ECommerce.ShippingService.Shippings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.ShippingService.Shippings
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShippingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddShippingCommand command)
        {
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailure)
            {
                return BadRequest(commandResult.Error);
            }

            return CreatedAtAction(nameof(Post), new { id = commandResult.Value }, commandResult.Value);
        }

        [HttpGet]
        [Route("byOrderId/{orderId}")]
        public async Task<IActionResult> GetByOrderId([FromQuery] int orderId)
        {
            var shippingByOrderIdQuery = new GetShippingByOrderIdQuery(orderId);
            var shipping = await _mediator.Send(shippingByOrderIdQuery);

            if (shipping == null)
                return NotFound();

            return Ok(shipping);

        }
    }
}
