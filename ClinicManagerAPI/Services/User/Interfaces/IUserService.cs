using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.User;

namespace ClinicManagerAPI.Services.User.Interfaces
{
    /// <summary>
    /// Provides operations related to user management, including retrieval,
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        Task<bool> DeleteUser(int id);

        /// <summary>
        /// Retrieves the authenticated user's details from the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context containing the user's claims.</param>
        /// <returns>The authenticated user's ID, email, and role.</returns>
        UserAuthenticatedDto GetAuthenticatedUser(HttpContext httpContext);

        /// <summary>
        /// Retrieves a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A <see cref="UserDto"/> representing the user.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        Task<UserDto> GetUserById(int id);

        /// <summary>
        /// Gets a paginated list of users. Only accessible by admin users.
        /// </summary>
        /// <param name="role">The role of the current user. Must be Admin to access this method.</param>
        /// <param name="parameters">The parameters used to filter and paginate the user list.</param>
        /// <returns>A paged result containing user data as <see cref="UserDto"/> objects.</returns>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when the role is not Admin.</exception>
        Task<PagedResult<UserDto>> GetUsers(UserRole role, QueryUserParameters parameters);

        /// <summary>
        /// Retrieves a user along with their associated appointments by user ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="UserDto"/> representing the user with their appointments.</returns>
        Task<UserDto> GetUserWithAppointments(int id);

        /// <summary>
        /// Updates the data of an existing user.
        /// </summary>
        /// <param name="userDto">The updated user data.</param>
        /// <returns>The updated <see cref="UserDto"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="Exception">Thrown when the update operation fails.</exception>
        Task<UserDto> UpdateUser(int userId, UserRole role, UserUpdateDto userUpdateDto);
    }
}