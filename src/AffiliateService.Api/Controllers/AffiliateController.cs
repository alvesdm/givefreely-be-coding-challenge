using AffiliateService.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AffiliateController : ControllerBase
    {
        public AffiliateController()
        {
        }

        /// <summary>
        /// Fetch an affliate.
        /// </summary>
        [HttpGet("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid uniqueId)
        {
            return Ok();
        }

        /// <summary>
        /// Get a count of all customers that belong to a specific affiliate.
        /// </summary>
        [HttpGet("{uniqueId}/customers/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItsCustomersCount(Guid uniqueId)
        {
            return Ok();
        }

        /// <summary>
        /// Get all customers that belong to a specific affiliate, in a paged list.
        /// </summary>
        [HttpGet("{uniqueId}/customers/{page:int:min(1)}/{pageSize:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItsCustomers([FromRoute] Guid uniqueId, int page = 1, int pageSize = 10)
        {
            return Ok();
        }

        /// <summary>
        /// Get all Affiliates in a paged list
        /// </summary>
        [HttpGet("{page:int:min(1)}/{pageSize:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ListQueryParameters query, int page = 1, int pageSize = 10)
        {
            return Ok();
        }

        /// <summary>
        /// Insert an affiliate.
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(InsertUpdateAffiliate affiliate)
        {
            var resultId = Guid.NewGuid();
            return CreatedAtAction(nameof(Get), new { resultId }, resultId);
        }

        /// <summary>
        /// Update an affiliate.
        /// </summary>
        [HttpPut("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] InsertUpdateAffiliate affiliate, Guid uniqueId)
        {
            return NoContent();
        }

        /// <summary>
        /// Remove an affiliate.
        /// </summary>
        [HttpDelete("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid uniqueId)
        {
            return NoContent();
        }
    }
}
