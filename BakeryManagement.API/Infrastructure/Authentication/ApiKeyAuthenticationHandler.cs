using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using InterviewChallenge.API.Domain.BusinessException;
using InterviewChallenge.API.Common;

namespace InterviewChallenge.API.Infrastructure.Authentication
{
    /// <summary>
    /// A custom authentication handler for API key authentication.
    /// Inherits from AuthenticationHandler&lt;AuthenticationSchemeOptions&gt;.
    /// </summary>
    internal sealed class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IApiKeyValidator _apiKeyValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyAuthenticationHandler"/> class.
        /// </summary>
        /// <param name="options">The options for the authentication scheme.</param>
        /// <param name="logger">The logger factory for logging.</param>
        /// <param name="encoder">The URL encoder for encoding URLs.</param>
        /// <param name="clock">The system clock.</param>
        /// <param name="apiKeyValidator">The service for validating API keys.</param>
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApiKeyValidator apiKeyValidator)
            : base(options, logger, encoder, clock)
        {
            MethodParameterValidation.ValidateParameter(apiKeyValidator, nameof(apiKeyValidator));
            _apiKeyValidator = apiKeyValidator;
        }

        /// <summary>
        /// Handles the authentication of the incoming request.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with an <see cref="AuthenticateResult"/> as the result.</returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            AuthenticateResult authenticationResult;

            // Check for the presence of the API key in the request headers
            if (Request.Headers == null ||
                !Request.Headers.TryGetValue(Constants.AurhorizationKey, out var apiKey) || string.IsNullOrEmpty(apiKey))
            {
                // API key is missing
                authenticationResult = AuthenticateResult.Fail(Constants.MissingAuthorizationKeyHeader);
            }
            else if (_apiKeyValidator.IsValid(apiKey))
            {
                // API key is valid, create a ClaimsPrincipal
                var claims = new[] { new Claim(ClaimTypes.Name, Constants.ApiUser) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                authenticationResult = AuthenticateResult.Success(ticket);
            }
            else
            {
                // API key is invalid
                authenticationResult = AuthenticateResult.Fail(Constants.InvalidApiKey);
            }

            return Task.FromResult(authenticationResult);
        }
    }
}
