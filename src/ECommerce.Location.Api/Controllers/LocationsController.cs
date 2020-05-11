using ECommerce.Location.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Location.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpGet("ByZipCode/{zipCode}")]
        [ProducesResponseType(typeof(IEnumerable<GetLocationByZipCodeQueryResult>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByZipCode(int zipCode)
        {
            var query = new GetLocationByZipCodeQuery(zipCode);
            var locations = await _mediator.Send(query);

            if (locations == null || !locations.Any())
                return NotFound();

            return Ok(locations);
        }
    }
}
