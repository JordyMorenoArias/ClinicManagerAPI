using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Repositories
{
    /// <summary>
    /// Repository for managing user data.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ClinicManagerContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ClinicManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user if found; otherwise, <c>null</c>.</returns>
        public async Task<UserEntity?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Retrieves a user along with their associated appointments.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="UserEntity"/> object representing the user with their appointments, or <c>null</c> if no matching user is found.</returns>
        public async Task<UserEntity?> GetUserWithAppointments(int id)
        {
            return await _context.Users
                .Include(u => u.CreatedAppointments)
                .Include(u => u.DoctorAppointments)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns> A <see cref="UserEntity"/> object representing the user with the specified username, or <c>null</c> if no matching user is found. </returns>
        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user if found; otherwise, <c>null</c>.</returns>
        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Retrieves a paginated list of users based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>
        /// A <see cref="PagedResult{UserEntity}"/> object containing the list of users that match the 
        /// query parameters, along with pagination metadata such as total items, current page, and page size.
        /// </returns>
        public async Task<PagedResult<UserEntity>> GetUsers(QueryUserParameters parameters)
        {
            var query = _context.Users.AsQueryable();

            if (parameters.IsActive.HasValue)
                query = query.Where(u => u.IsActive == parameters.IsActive.Value);

            if (!string.IsNullOrEmpty(parameters.Role))
                query = query.Where(u => u.Role.ToString() == parameters.Role);

            if (!string.IsNullOrEmpty(parameters.FullName))
                query = query.Where(u => u.FullName.Contains(parameters.FullName));

            var totalItems = await query.CountAsync();
            var users = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResult<UserEntity>
            {
                Items = users,
                TotalItems = totalItems,
                Page = parameters.Page,
                PageSize = parameters.PageSize,
            };
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the added user.</returns>
        public async Task<UserEntity> AddUser(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="user">The user entity with updated data.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated user if found</returns>
        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="user">The user entity to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public async Task<bool> DeleteUser(UserEntity user)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}