using AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClinicManagerAPI.Services.User
{
    /// <summary>
    /// Provides operations related to user management, including retrieval,
    /// modification, deletion, and role assignment.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._passwordHasher = passwordHasher;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrieves the authenticated user's details from the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context containing the user's claims.</param>
        /// <returns>The authenticated user's ID, email, and role.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when required claims are missing or invalid.
        /// </exception>
        public UserAuthenticatedDto GetAuthenticatedUser(HttpContext httpContext)
        {
            var userIdClaim = httpContext.User.FindFirst("Id")?.Value;
            var userEmailClaim = httpContext.User.FindFirst("Email")?.Value;
            var userRoleClaim = httpContext.User.FindFirst("Role")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userEmailClaim) || string.IsNullOrEmpty(userRoleClaim))
                throw new UnauthorizedAccessException("Invalid token or unauthorized access.");

            return new UserAuthenticatedDto
            {
                Id = int.Parse(userIdClaim),
                Email = userEmailClaim,
                Role = Enum.Parse<UserRole>(userRoleClaim)
            };
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="UserDto"/> representing the user.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
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
        /// <param name="requesterRole">The role of the current user. Must be Admin to access this method.</param>
        /// <param name="parameters">The parameters used to filter and paginate the user list.</param>
        /// <returns>A paged result containing user data as <see cref="UserDto"/> objects.</returns>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when the role is not Admin.</exception>
        public async Task<PagedResult<UserDto>> GetUsers(UserRole requesterRole, QueryUserParameters parameters)
        {
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
        public async Task<UserDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            _mapper.Map(userUpdateDto, user);

            var userUpdated = await _userRepository.UpdateUser(user);
            return _mapper.Map<UserDto>(userUpdated);
        }

        /// <summary>
        /// Changes a user's password. Only accessible by admin users.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userChangePasswordDto"></param>
        /// <returns> The updated <see cref="UserDto"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<UserDto> ChangePassword(int userId, ChangeUserPasswordDto userChangePasswordDto)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            user.PasswordHash = _passwordHasher.HashPassword(user, userChangePasswordDto.NewPassword);
            var userUpdated = await _userRepository.UpdateUser(user);
            return _mapper.Map<UserDto>(userUpdated);
        }

        /// <summary>
        /// Assigns a new role to a user. Only accessible by admin users.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newRole"></param>
        /// <returns> The updated <see cref="UserDto"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<UserDto> ChangeUserRole(int userId, ChangeUserRoleDto newRole)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            user.Role = newRole.Role;
            var userUpdated = await _userRepository.UpdateUser(user);
            return _mapper.Map<UserDto>(userUpdated);
        }

        /// <summary>
        /// Deletes a user by their ID. Only accessible by admin users.
        /// </summary>
        /// <param name="requesterRole"></param>
        /// <param name="id"></param>
        /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c>.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<bool> DeleteUser(UserRole requesterRole, int id)
        {
            if (requesterRole != UserRole.admin)
                throw new UnauthorizedAccessException("Only admins can delete users.");

            var user = await _userRepository.GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return await _userRepository.DeleteUser(user);
        }
    }
}