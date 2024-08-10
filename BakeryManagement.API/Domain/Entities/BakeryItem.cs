using LiteDB;

namespace InterviewChallenge.API.Domain.Models
{
    /// <summary>
    /// Represents a bakery item in the system.
    /// </summary>
    public class BakeryItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the bakery item.
        /// </summary>
        [BsonId]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the bakery item.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the bakery item.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the bakery item available in stock.
        /// </summary>
        public int QuantityAvailable { get; set; }

        /// <summary>
        ///  Gets or sets the version of the order.
        /// </summary>
        public int Version { get; set; } = 0; // Version for optimistic concurrency
    }

}
