using ClinicManagerAPI.Models.DTOs;

namespace ClinicManagerAPI.Services.Security.Interfaces
{
    /// <summary>
    /// Interface for JWT (JSON Web Token) generation service.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for a user.
        /// </summary>
        /// <param name="user">The user information used to generate the token.</param>
        /// <param name="expires">The expiration date and time for the token.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        string GenerateJwtToken(UserGenerateTokenDto user, DateTime expires);
    }
}