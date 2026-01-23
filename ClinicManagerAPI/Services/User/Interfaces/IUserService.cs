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
        /// Assigns a new role to a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newRole"></param>
        /// <returns> The updated <see cref="UserDto"/>.</returns>
        Task<UserDto> ChangeUserRole(int userId, ChangeUserRoleDto newRole);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userChangePasswordDto"></param>
        /// <returns> The updated <see cref="UserDto"/>.</returns>
        Task<UserDto> ChangePassword(int userId, ChangeUserPasswordDto userChangePasswordDto);

        /// <summary>
        /// Retrieves the authenticated user's details from the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context containing the user's claims.</param>
        /// <returns>The authenticated user's ID, email, and role.</returns>
        UserAuthenticatedDto GetAuthenticatedUser(HttpContext httpContext);

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="UserDto"/> representing the user.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<UserDto> GetUserById(int id);

        /// <summary>
        /// Updates a user's information.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userUpdateDto"></param>
        /// <returns> The updated <see cref="UserDto"/>.</returns>
        Task<UserDto> UpdateUser(int id, UpdateUserDto userUpdateDto);

        /// <summary>
        /// Delete a patient allergy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="OperationResult"/> indicating the result of the deletion.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<PagedResult<UserDto>> GetUsers(UserQueryParameters parameters);

        /// <summary>
        /// Deletes a user by their ID. Only accessible by admin users.
        /// </summary>
        /// <param name="requesterRole"></param>
        /// <param name="id"></param>
        /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<bool> DeleteUser(int id);
    }
}