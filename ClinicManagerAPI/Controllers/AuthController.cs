using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user and returns a token.
        /// </summary>
        /// <param name="userLogin">Login credentials.</param>
        /// <returns>JWT and user info if successful.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userLogin)
        {
            var result = await _authService.Login(userLogin);

            Response.Cookies.Append("access_token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = result.Expires
            });

            return Ok(new
            {
                user = result.User,
            });
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="userRegister">User registration info.</param>
        /// <returns>Status message indicating success or failure.</returns>
        [HttpPost("register")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userRegister)
        {
            var result = await _authService.Register(userRegister);
            return Ok(result);
        }

        /// <summary>
        /// Logs out the current user by deleting the authentication cookie.
        /// </summary>
        /// <returns> Status message indicating successful logout.</returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token", new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
            });

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            });
        }
    }
}