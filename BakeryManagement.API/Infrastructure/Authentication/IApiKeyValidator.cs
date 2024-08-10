namespace InterviewChallenge.API.Infrastructure.Authentication
{
    /// <summary>
    /// Interface for validating API keys.
    /// </summary>
    public interface IApiKeyValidator
    {
        /// <summary>
        /// Validates the provided API key.
        /// </summary>
        /// <param name="apiKey">The API key to validate.</param>
        /// <returns>True if the API key is valid; otherwise, false.</returns>
        bool IsValid(string apiKey);
    }
}
