using InterviewChallenge.API.Common;

namespace InterviewChallenge.API.Infrastructure.Authentication
{
    /// <summary>
    /// A class that validates API keys based on configuration settings.
    /// Implements the IApiKeyValidator interface.
    /// </summary>
    internal class ApiKeyValidator : IApiKeyValidator
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyValidator"/> class.
        /// </summary>
        /// <param name="configuration">The configuration service for retrieving the API key from configuration settings.</param>
        public ApiKeyValidator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Validates the provided API key against the configured API key.
        /// </summary>
        /// <param name="apiKey">The API key to validate.</param>
        /// <returns>True if the provided API key matches the configured API key; otherwise, false.</returns>
        bool IApiKeyValidator.IsValid(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));
            }

            string actualApiKey = _configuration.GetValue<string>(Constants.ApiKey);
            return actualApiKey == apiKey;
        }
    }
}
