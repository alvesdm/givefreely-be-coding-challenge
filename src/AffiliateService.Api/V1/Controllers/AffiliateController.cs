using AffiliateService.Api.Models;
using AffiliateService.Api.V1.Controllers.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateService.Api.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AffiliateController : ControllerBase
    {
        /// <summary>
        /// - Using mediator pattern here to avoid fat controllers.
        /// This way our controllers keep clean and no other concerns 
        /// than just work as a facade to spit requests/resposes out.
        /// 
        /// - Validation, validation errors and unhandled exceptions are handled
        /// via middlewares, to contribute with the clean controllers style.
        /// </summary>
        private readonly IMediator _mediator;

        public AffiliateController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Fetch an affliate.
        /// </summary>
        /// <remarks>
        /// Note: PKs shouldn't be exposed to the outside world, therefore an unique identifier instead.
        /// </remarks>
        [HttpGet("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAffiliateRequest(uniqueId), cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Get all customers that belong to a specific affiliate, in a paged list.
        /// </summary>
        [HttpGet("{uniqueId}/customers/{page:int:min(1)}/{pageSize:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItsCustomers([FromRoute] Guid uniqueId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new QueryAffiliateCustomersRequest(uniqueId, page, pageSize));

            return Ok(result);
        }

        /// <summary>
        /// Get all Affiliates in a paged list
        /// </summary>
        [HttpGet("{page:int:min(1)}/{pageSize:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ListQueryParameters query, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new QueryAffiliateRequest(query.criteria ?? string.Empty, page, pageSize), cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Insert an affiliate.
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(InsertUpdateAffiliate affiliate, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new AddAffiliateRequest(affiliate), cancellationToken);

            return CreatedAtAction(nameof(Get), new { result.UniqueId }, result.UniqueId);
        }

        /// <summary>
        /// Update an affiliate.
        /// </summary>
        [HttpPut("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] InsertUpdateAffiliate affiliate, Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new UpdateAffiliateRequest(affiliate, uniqueId), cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Remove an affiliate.
        /// </summary>
        [HttpDelete("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new RemoveAffiliateRequest(uniqueId), cancellationToken);

            return NoContent();
        }
    }
}
