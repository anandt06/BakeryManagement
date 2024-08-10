using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Domain.Models;
using LiteDB;

namespace InterviewChallenge.API.Infrastructure.Data
{
    /// <summary>
    /// Service layer for managing LiteDB collections.
    /// Implements access to BakeryItem and Order collections using LiteDB.
    /// </summary>
    public class LiteDBService : ILiteDBService
    {
        private readonly LiteDatabase _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDBService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration containing the connection string for LiteDB.</param>
        public LiteDBService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LiteDB");
            _db = new LiteDatabase(connectionString);
        }

        /// <summary>
        /// Gets the collection of bakery items from LiteDB.
        /// </summary>
        public ILiteCollection<BakeryItem> BakeryItems => _db.GetCollection<BakeryItem>("bakeryItems");

        /// <summary>
        /// Gets the collection of orders from LiteDB.
        /// </summary>
        public ILiteCollection<Order> Orders => _db.GetCollection<Order>("orders");
    }
}
