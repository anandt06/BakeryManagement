namespace InterviewChallenge.API.Common
{

    /// <summary>
    /// Contains constant values used for API key management and authentication.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// The name of the user for API access.
        /// </summary>
        public const string ApiUser = "ApiUser";

        /// <summary>
        /// Error message indicating that the Authorization header is missing in the request.
        /// </summary>
        public const string MissingAuthorizationKeyHeader = "Missing Authorization Key header";

        /// <summary>
        /// Error message indicating that the provided API key is invalid.
        /// </summary>
        public const string InvalidApiKey = "Invalid API key";

        /// <summary>
        /// The name of the Authorization header used in HTTP requests.
        /// </summary>
        public const string AurhorizationKey = "Authorization";

        /// <summary>
        /// The name of the API key used for authentication.
        /// </summary>
        public const string ApiKey = "ApiKey";

        /// <summary>
        /// The scheme or type used for API key authentication.
        /// </summary>
        public const string ApiKeyAuthentication = "ApiKeyAuthentication";
    }

}