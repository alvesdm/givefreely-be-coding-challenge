using AffiliateService.Api.Models;
using AffiliateService.Api.V1.Controllers.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateService.Api.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Fetch a costumer.
        /// </summary>
        /// <remarks>
        /// Note: PKs shouldn't be exposed to the outside world, therefore an unique identifier instead.
        /// </remarks>
        [HttpGet("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCustomerRequest(uniqueId), cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Get all customers in a paged list.
        /// </summary>
        [HttpGet("{page:int:min(1)}/{pageSize:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ListQueryParameters query, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new QueryCustomerRequest(query.criteria ?? string.Empty, page, pageSize), cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Insert a customer.
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(InsertUpdateCustomer Customer, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new AddCustomerRequest(Customer), cancellationToken);

            return CreatedAtAction(nameof(Get), new { result.UniqueId }, result.UniqueId);
        }

        /// <summary>
        /// Update a customer.
        /// </summary>
        [HttpPut("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] InsertUpdateCustomer Customer, Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new UpdateCustomerRequest(Customer, uniqueId), cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Remove a customer.
        /// </summary>
        [HttpDelete("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new RemoveCustomerRequest(uniqueId), cancellationToken);

            return NoContent();
        }
    }
}
