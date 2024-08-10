using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewChallenge.API.Controllers
{

    // The BakeryItemController handles CRUD operations for bakery items. It communicates with an IBakeryItemService to perform these operations.
    // Each method is responsible for a specific action, such as retrieving all bakery items, getting a specific bakery item, creating a new bakery item,
    // updating an existing bakery item, or deleting a bakery item. The controller returns appropriate HTTP status codes depending on the result of each operation.
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BakeryItemController : ControllerBase
    {
        private readonly IBakeryItemService _bakeryItemService;

        public BakeryItemController(IBakeryItemService bakeryItemService)
        {
            _bakeryItemService = bakeryItemService;
        }

        /// <summary>
        /// Retrieves all bakery items.
        /// </summary>
        /// <returns>A list of bakery items with an HTTP 200 status if successful, or an HTTP 500 status if an error occurs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BakeryItem>>> GetAll()
        {
            try
            {
                var items = await _bakeryItemService.GetAllBakeryItemsAsync();
                return Ok(items);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific bakery item by its ID.
        /// </summary>
        /// <param name="id">The ID of the bakery item to retrieve.</param>
        /// <returns>The bakery item with the specified ID, an HTTP 404 status if not found, or an HTTP 500 status if an error occurs.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BakeryItem>> Get(int id)
        {
            try
            {
                var item = await _bakeryItemService.GetBakeryItemAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new bakery item.
        /// </summary>
        /// <param name="item">The bakery item object to create.</param>
        /// <returns>The created bakery item with an HTTP 201 status, or an HTTP 400 status if the input is invalid.</returns>
        [HttpPost]
        public async Task<ActionResult<BakeryItem>> Create([FromBody] BakeryItem item)
        {
            try
            {
                var createdItem = await _bakeryItemService.CreateBakeryItemAsync(item);
                return CreatedAtAction(nameof(Get), new { id = createdItem.Id }, createdItem);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// Updates an existing bakery item by its ID.
        /// </summary>
        /// <param name="id">The ID of the bakery item to update.</param>
        /// <param name="item">The updated bakery item object.</param>
        /// <returns>An HTTP 204 status if the update was successful, 404 if not found, or 400/500 if an error occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BakeryItem item)
        {
            try
            {
                var result = await _bakeryItemService.UpdateBakeryItemAsync(id, item);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a bakery item by its ID.
        /// </summary>
        /// <param name="id">The ID of the bakery item to delete.</param>
        /// <returns>An HTTP 204 status if deleted, 404 if not found, or 400/500 if an error occurs.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _bakeryItemService.DeleteBakeryItemAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
