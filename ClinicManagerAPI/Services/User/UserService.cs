using AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Repositories;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using EcommerceAPI.Models.DTOs.User;

namespace ClinicManagerAPI.Services.User
{
    /// <summary>
    /// Provides operations related to user management, including retrieval,
    /// modification, deletion, and role assignment.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrieves a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A <see cref="UserDto"/> representing the user.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Gets a paginated list of users. Only accessible by admin users.
        /// </summary>
        /// <param name="role">The role of the current user. Must be Admin to access this method.</param>
        /// <param name="parameters">The parameters used to filter and paginate the user list.</param>
        /// <returns>A paged result containing user data as <see cref="UserDto"/> objects.</returns>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when the role is not Admin.</exception>
        public async Task<PagedResult<UserDto>> GetUsers(UserRole role, QueryUserParameters parameters)
        {
            if (role != UserRole.admin)
                throw new UnauthorizedAccessException("Only admins can retrieve all users.");

            var users = await _userRepository.GetUsers(parameters);

            var userDtos = _mapper.Map<List<UserDto>>(users.Items);

            return new PagedResult<UserDto>
            {
                Items = userDtos,
                TotalItems = users.TotalItems,
                Page = users.Page,
                PageSize = users.PageSize
            };
        }

        /// <summary>
        /// Updates the data of an existing user.
        /// </summary>
        /// <param name="userDto">The updated user data.</param>
        /// <returns>The updated <see cref="UserDto"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="Exception">Thrown when the update operation fails.</exception>
        public async Task<UserDto> UpdateUser(int userId, UserRole role, UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            if (userId != user.Id && role != UserRole.admin)
                throw new UnauthorizedAccessException("You do not have permission to update this user.");

            _mapper.Map(userUpdateDto, user);

            var userUpdated = await _userRepository.UpdateUser(user);

            if (userUpdated is null)
                throw new Exception("Failed to update user.");

            return _mapper.Map<UserDto>(userUpdated);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return await _userRepository.DeleteUser(user);
        }
    }
}