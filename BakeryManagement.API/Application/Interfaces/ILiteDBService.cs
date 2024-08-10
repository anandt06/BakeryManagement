using InterviewChallenge.API.Domain.Models;
using LiteDB;

namespace InterviewChallenge.API.Application.Interfaces
{
    /// <summary>
    /// Interface for accessing collections in a LiteDB database.
    /// Provides properties for managing bakery items and orders.
    /// </summary>
    public interface ILiteDBService
    {
        /// <summary>
        /// Gets the collection of bakery items from the LiteDB database.
        /// </summary>
        /// <value>An <see cref="ILiteCollection{BakeryItem}"/> representing the bakery items collection.</value>
        ILiteCollection<BakeryItem> BakeryItems { get; }

        /// <summary>
        /// Gets the collection of orders from the LiteDB database.
        /// </summary>
        /// <value>An <see cref="ILiteCollection{Order}"/> representing the orders collection.</value>
        ILiteCollection<Order> Orders { get; }
    }
}

