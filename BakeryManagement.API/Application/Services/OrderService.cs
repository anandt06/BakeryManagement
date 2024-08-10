using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Domain.Models;

namespace InterviewChallenge.API.Application.Services
{
    /// <summary>
    /// Service layer for managing orders.
    /// Implements business logic for creating, retrieving, and deleting orders.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderRepository">The repository to access order data.</param>
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The created order.</returns>
        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            try
            {
                return await _orderRepository.CreateOrderAsync(order);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException("An error occurred while creating the order.", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific order by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, or throws a KeyNotFoundException if not found.</returns>
        public async Task<Order> GetOrderAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            try
            {
                var order = await _orderRepository.GetOrderAsync(id);
                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with id {id} not found.");
                }
                return order;
            }
            catch (Exception ex) when (!(ex is KeyNotFoundException))
            {
                // Log the exception here
                throw new ApplicationException($"An error occurred while retrieving the order with id {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves all orders asynchronously.
        /// </summary>
        /// <returns>A collection of all orders.</returns>
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _orderRepository.GetAllOrdersAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException("An error occurred while retrieving all orders.", ex);
            }
        }

        /// <summary>
        /// Deletes an order by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteOrderAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            try
            {
                return await _orderRepository.DeleteOrderAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException($"An error occurred while deleting the order with id {id}.", ex);
            }
        }
    }

}
