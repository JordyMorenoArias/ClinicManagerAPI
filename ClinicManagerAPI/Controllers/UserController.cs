using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>The user data.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] QueryUserParameters parameters)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var users = await _userService.GetUsers(userAuthenticated.Role, parameters);
            return Ok(users);
        }

        /// <summary>
        /// Updates the authenticated user's information.
        /// </summary>
        /// <param name="userUpdateDto">The user data to update.</param>
        /// <returns>The updated user data.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "canUpdateUser")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var user = await _userService.UpdateUser(userAuthenticated.Id, userAuthenticated.Role, id, userUpdateDto);
            return Ok(user);
        }


        /// <summary>
        /// Changes the authenticated user's password.
        /// </summary>
        /// <param name="userChangePasswordDto"></param>
        /// <returns> The user data after password change.</returns>
        [HttpPut("{id}/change-password")]
        [Authorize(Policy = "canUpdateUser")]
        public async Task<IActionResult> ChangePassword([FromBody] int id, [FromBody] ChangeUserPasswordDto userChangePasswordDto)
        {
            var user = await _userService.ChangePassword(id, userChangePasswordDto);
            return Ok(user);
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeUserRole([FromRoute] int id, [FromBody] ChangeUserRoleDto userChangeRoleDto)
        {
            var user = await _userService.ChangeUserRole(id, userChangeRoleDto);
            return Ok(user);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// Only administrators or the user themselves can delete.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A success message upon deletion.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            await _userService.DeleteUser(userAuthenticated.Role, id);
            return Ok("User deleted successfully.");
        }
    }
}