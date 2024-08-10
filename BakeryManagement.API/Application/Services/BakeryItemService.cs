using InterviewChallenge.API.Application.Interfaces;
using InterviewChallenge.API.Domain.Models;

namespace InterviewChallenge.API.Application.Services
{
    /// <summary>
    /// Service layer for managing bakery items.
    /// Implements business logic for creating, retrieving, updating, and deleting bakery items.
    /// </summary>
    public class BakeryItemService : IBakeryItemService
    {
        private readonly IBakeryItemRepository _bakeryItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BakeryItemService"/> class.
        /// </summary>
        /// <param name="bakeryItemRepository">The repository to access bakery items data.</param>
        public BakeryItemService(IBakeryItemRepository bakeryItemRepository)
        {
            _bakeryItemRepository = bakeryItemRepository ?? throw new ArgumentNullException(nameof(bakeryItemRepository));
        }

        /// <summary>
        /// Creates a new bakery item asynchronously.
        /// </summary>
        /// <param name="item">The bakery item to create.</param>
        /// <returns>The created bakery item.</returns>
        public async Task<BakeryItem> CreateBakeryItemAsync(BakeryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                return await _bakeryItemRepository.CreateBakeryItemAsync(item);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException("An error occurred while creating the bakery item.", ex);
            }
        }

        /// <summary>
        /// Deletes a bakery item by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bakery item to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteBakeryItemAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            try
            {
                return await _bakeryItemRepository.DeleteBakeryItemAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException($"An error occurred while deleting the bakery item with id {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific bakery item by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bakery item to retrieve.</param>
        /// <returns>The bakery item with the specified ID, or null if not found.</returns>
        public async Task<BakeryItem> GetBakeryItemAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            try
            {
                return await _bakeryItemRepository.GetBakeryItemAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException($"An error occurred while retrieving the bakery item with id {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves all bakery items asynchronously.
        /// </summary>
        /// <returns>A collection of all bakery items.</returns>
        public async Task<IEnumerable<BakeryItem>> GetAllBakeryItemsAsync()
        {
            try
            {
                return await _bakeryItemRepository.GetAllBakeryItemsAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException("An error occurred while retrieving all bakery items.", ex);
            }
        }

        /// <summary>
        /// Updates an existing bakery item by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the bakery item to update.</param>
        /// <param name="item">The updated bakery item object.</param>
        /// <returns>A boolean indicating whether the update was successful.</returns>
        public async Task<bool> UpdateBakeryItemAsync(int id, BakeryItem item)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id", nameof(id));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (id != item.Id)
            {
                throw new ArgumentException("Id mismatch", nameof(id));
            }

            try
            {
                return await _bakeryItemRepository.UpdateBakeryItemAsync(id, item);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new ApplicationException($"An error occurred while updating the bakery item with id {id}.", ex);
            }
        }
    }
}
