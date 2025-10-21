using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    /// <summary>
    /// Provides data access methods for user entities.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user to the data store.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A task representing the asynchronous operation. The task result contains the added user.</returns>
        Task<UserEntity> AddUser(UserEntity user);

        /// <summary>
        /// Deletes a user from the data store.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteUser(UserEntity user);

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user if found; otherwise, <c>null</c>.</returns>
        Task<UserEntity?> GetUserByEmail(string email);

        /// <summary>
        /// Retrieves a user by their unique ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user if found; otherwise, <c>null</c>.</returns
        Task<UserEntity?> GetUserById(int id);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns> A <see cref="UserEntity"/> object representing the user with the specified username, or <c>null</c> if no matching user is found. </returns>
        Task<UserEntity?> GetUserByUsername(string username);

        /// <summary>
        /// Retrieves a paginated list of users based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>
        /// A <see cref="PagedResult{UserEntity}"/> object containing the list of users that match the 
        /// query parameters, along with pagination metadata such as total items, current page, and page size.
        /// </returns>
        Task<PagedResult<UserEntity>> GetUsers(QueryUserParameters parameters);

        /// <summary>
        /// Retrieves a user along with their associated appointments.
        /// </summary>
        /// <param name="id"></param>
        Task<UserEntity?> GetUserWithAppointments(int id);

        /// <summary>
        /// Updates an existing user's data in the data store.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated user if found</returns>
        Task<UserEntity> UpdateUser(UserEntity user);
    }
}