using ClinicManagerAPI.Models.DTOs.Auth;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.User;

namespace ClinicManagerAPI.Services.Auth.Interfaces
{
    /// <summary>
    /// Interface for authentication service that handles user authentication operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Logs the user in using the provided credentials.
        /// </summary>
        /// <param name="loginDto">The login data transfer object containing the user's credentials.</param>
        /// <returns>A task representing the asynchronous operation, with an <see cref="AuthResultDto"/> containing the authentication details.</returns>
        Task<AuthResultDto> Login(LoginUserDto loginDto);

        /// <summary>
        /// Registers the specified user register.
        /// </summary>
        /// <param name="userRegister">The user register.</param>
        /// <returns>A task representing the asynchronous operation, with an <see cref="OperationResult"/> indicating the result of the registration process.</returns>
        Task<OperationResult> Register(RegisterUserDto userRegister);
    }
}