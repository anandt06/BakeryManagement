namespace InterviewChallenge.API.Domain.BusinessException
{
    /// <summary>
    /// Provides methods for validating method parameters.
    /// </summary>
    internal static class MethodParameterValidation
    {
        /// <summary>
        /// Validates that the specified parameter is not null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The parameter value to validate.</param>
        /// <param name="parameterName">The name of the parameter to include in the exception message if validation fails.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="value"/> is null.</exception>
        internal static void ValidateParameter<T>(T value, string parameterName) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }

}