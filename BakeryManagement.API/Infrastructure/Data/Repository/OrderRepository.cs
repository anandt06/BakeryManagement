using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Domain.Models;

namespace InterviewChallenge.API.Infrastructure.Data.Data.Repository
{
    /// <summary>
    /// Repository for managing Order data in LiteDB.
    /// Implements CRUD operations for Order entities.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly ILiteDBService _dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="dbService">The service for accessing LiteDB collections.</param>
        public OrderRepository(ILiteDBService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        /// <summary>
        /// Creates a new order and inserts it into the database.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The created order.</returns>
        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.Version = 0; // Set the initial version
            await Task.Run(() => _dbService.Orders.Insert(order));
            return order;
        }

        /// <summary>
        /// Retrieves an order by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public async Task<Order> GetOrderAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            return await Task.Run(() => _dbService.Orders.FindById(id));
        }

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>A collection of all orders.</returns>
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await Task.Run(() => _dbService.Orders.FindAll());
        }

        /// <summary>
        /// Updates an existing order in the database.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The updated order data.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(int id, Order order)
        {
            if (id <= 0 || order == null || id != order.Id)
            {
                throw new ArgumentException("Invalid id or order data", nameof(id));
            }

            var existingOrder = await GetOrderAsync(id);
            if (existingOrder == null)
            {
                return false;
            }

            if (existingOrder.Version != order.Version)
            {
                // Version mismatch indicates a concurrency conflict
                throw new InvalidOperationException("Order has been modified by another process.");
            }

            // Increment the version
            order.Version++;

            return await Task.Run(() => _dbService.Orders.Update(order));
        }

        /// <summary>
        /// Deletes an order from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>True if the delete was successful; otherwise, false.</returns>
        public async Task<bool> DeleteOrderAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            return await Task.Run(() => _dbService.Orders.Delete(id));
        }
    }
}
