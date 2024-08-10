using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewChallenge.API.Controllers
{

    // The OrderController handles CRUD operations for orders. It communicates with an IOrderService to perform these operations.
    // Each method is responsible for a specific action, such as retrieving all orders, getting a specific order, creating a new order,
    // or deleting an existing order. The controller returns appropriate HTTP status codes depending on the result of each operation.
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A list of orders with an HTTP 200 status if successful, or an HTTP 500 status if an error occurs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, an HTTP 404 status if not found, or an HTTP 500 status if an error occurs.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
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
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order object to create.</param>
        /// <returns>The created order with an HTTP 201 status, or an HTTP 400 status if the input is invalid.</returns>
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(order);
                return CreatedAtAction(nameof(Get), new { id = createdOrder.Id }, createdOrder);
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
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>An HTTP 204 status if deleted, 404 if not found, or 400/500 if an error occurs.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _orderService.DeleteOrderAsync(id);
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
