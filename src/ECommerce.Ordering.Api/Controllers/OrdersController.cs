using ECommerce.Ordering.Api.Application.Commands;
using ECommerce.Ordering.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpGet("ByBuyerId/{buyerId}")]
        [ProducesResponseType(typeof(OrdersByBuyerIdQueryResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<OrdersByBuyerIdQueryResult>>> GetByBuyerId(int buyerId)
        {
            var ordersByBuyerIdQuery = new OrdersByBuyerIdQuery(buyerId);
            var orders = await _mediator.Send(ordersByBuyerIdQuery);

            if (!orders.Any())
                return NotFound();

            return Ok(orders);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(OrdersByBuyerIdQueryResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersByBuyerIdQueryResult>>> Post(AddOrderCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}