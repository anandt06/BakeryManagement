using InterviewChallenge.API.Domain.Models;

namespace InterviewChallenge.API.Application.Interfaces
{
    /// <summary>
    /// Interface for the service layer that manages bakery items.
    /// Provides methods for retrieving, creating, updating, and deleting bakery items.
    /// </summary>
    public interface IBakeryItemService
    {
        /// <summary>
        /// Retrieves all bakery items asynchronously.
        /// </summary>
        /// <returns>A collection of all bakery items.</returns>
        public Task<IEnumerable<BakeryItem>> GetAllBakeryItemsAsync();

        /// <summary>
        /// Retrieves a specific bakery item by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bakery item to retrieve.</param>
        /// <returns>The bakery item with the specified ID, or null if not found.</returns>
        public Task<BakeryItem> GetBakeryItemAsync(int id);

        /// <summary>
        /// Creates a new bakery item asynchronously.
        /// </summary>
        /// <param name="item">The bakery item to create.</param>
        /// <returns>The created bakery item.</returns>
        public Task<BakeryItem> CreateBakeryItemAsync(BakeryItem item);

        /// <summary>
        /// Updates an existing bakery item by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bakery item to update.</param>
        /// <param name="item">The updated bakery item object.</param>
        /// <returns>A boolean indicating whether the update was successful.</returns>
        public Task<bool> UpdateBakeryItemAsync(int id, BakeryItem item);

        /// <summary>
        /// Deletes a bakery item by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bakery item to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public Task<bool> DeleteBakeryItemAsync(int id);
    }

}
