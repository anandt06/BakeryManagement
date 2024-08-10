using LiteDB;

namespace InterviewChallenge.API.Domain.Models
{
    /// <summary>
    /// Represents an order in the bakery system.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        [BsonId]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the bakery item associated with the order.
        /// </summary>
        public int BakeryItemId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the bakery item ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer who placed the order.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or sets the version of the order.
        /// </summary>
        public int Version { get; set; } = 0;// Version for optimistic concurrency
    }

}
