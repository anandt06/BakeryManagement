﻿using InterviewChallenge.API.Domain.Models;

namespace InterviewChallenge.API.Application.Interfaces
{
    /// <summary>
    /// Interface for the service layer that manages orders.
    /// Provides methods for retrieving, creating, and deleting orders.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Retrieves all orders asynchronously.
        /// </summary>
        /// <returns>A collection of all orders.</returns>
        public Task<IEnumerable<Order>> GetAllOrdersAsync();

        /// <summary>
        /// Retrieves a specific order by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public Task<Order> GetOrderAsync(int id);

        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The created order.</returns>
        public Task<Order> CreateOrderAsync(Order order);

        /// <summary>
        /// Deletes an order by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public Task<bool> DeleteOrderAsync(int id);
    }

}
