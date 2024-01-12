using AffiliateService.Api.V1.Controllers.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateService.Api.V1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportingController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Report number of customers referred by affiliate, in a paged list
        /// </summary>
        [HttpGet("affiliates/customers/count/{page:int:min(1)}/{pageSize:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAffiliateCustomersCountCount([FromRoute] int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAffiliateCustomersCountReportRequest(page, pageSize), cancellationToken);

            return Ok(result);
        }
    }
}
