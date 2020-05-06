using ECommerce.Inventory.Api.Application.Commands;
using ECommerce.Inventory.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductByIdQueryResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(AddProductCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}