using AffiliateService.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {
        }

        /// <summary>
        /// Fetch a costumer.
        /// </summary>
        [HttpGet("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid uniqueId)
        {
            return Ok();
        }

        /// <summary>
        /// Get all customers in a paged list.
        /// </summary>
        [HttpGet("{page:int:min(1)}/{pageSize:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] ListQueryParameters query, int page = 1, int pageSize = 10)
        {
            return Ok();
        }

        /// <summary>
        /// Insert a customer.
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(InsertUpdateCustomer Customer)
        {
            var resultId = Guid.NewGuid();
            return CreatedAtAction(nameof(Get), new { resultId }, resultId);
        }

        /// <summary>
        /// Update a customer.
        /// </summary>
        [HttpPut("{uniqueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] InsertUpdateCustomer Customer, Guid uniqueId)
        {
            return NoContent();
        }

        /// <summary>
        /// Remove a customer.
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
