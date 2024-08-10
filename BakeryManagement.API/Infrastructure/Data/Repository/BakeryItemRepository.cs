using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Domain.Models;

namespace InterviewChallenge.API.Infrastructure.Data.Data.Repository
{
    /// <summary>
    /// Repository for managing BakeryItem data in LiteDB.
    /// Implements CRUD operations for BakeryItem entities.
    /// </summary>
    public class BakeryItemRepository : IBakeryItemRepository
    {
        private readonly ILiteDBService _dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BakeryItemRepository"/> class.
        /// </summary>
        /// <param name="dbService">The service for accessing LiteDB collections.</param>
        public BakeryItemRepository(ILiteDBService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        /// <summary>
        /// Creates a new bakery item and inserts it into the database.
        /// </summary>
        /// <param name="item">The bakery item to create.</param>
        /// <returns>The created bakery item.</returns>
        public async Task<BakeryItem> CreateBakeryItemAsync(BakeryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Version = 0; // Set the initial version
            await Task.Run(() => _dbService.BakeryItems.Insert(item));
            return item;
        }

        /// <summary>
        /// Deletes a bakery item from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the bakery item to delete.</param>
        /// <returns>True if the delete was successful; otherwise, false.</returns>
        public async Task<bool> DeleteBakeryItemAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            return await Task.Run(() => _dbService.BakeryItems.Delete(id));
        }

        /// <summary>
        /// Retrieves a bakery item by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the bakery item to retrieve.</param>
        /// <returns>The bakery item with the specified ID, or null if not found.</returns>
        public async Task<BakeryItem> GetBakeryItemAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            return await Task.Run(() => _dbService.BakeryItems.FindById(id));
        }

        /// <summary>
        /// Retrieves all bakery items from the database.
        /// </summary>
        /// <returns>A collection of all bakery items.</returns>
        public async Task<IEnumerable<BakeryItem>> GetAllBakeryItemsAsync()
        {
            return await Task.Run(() => _dbService.BakeryItems.FindAll());
        }

        /// <summary>
        /// Updates an existing bakery item in the database.
        /// </summary>
        /// <param name="id">The ID of the bakery item to update.</param>
        /// <param name="item">The updated bakery item data.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public async Task<bool> UpdateBakeryItemAsync(int id, BakeryItem bakeryItem)
        {
            if (id != bakeryItem.Id)
                return false;

            var existingItem = await GetBakeryItemAsync(id);
            if (existingItem == null)
                return false;

            if (existingItem.Version != bakeryItem.Version)
            {
                // Version mismatch indicates a concurrency conflict
                throw new InvalidOperationException("Item has been modified by another process.");
            }

            // Increment the version
            bakeryItem.Version++;
            return await Task.Run(() => _dbService.BakeryItems.Update(bakeryItem));
        }
    }
}
