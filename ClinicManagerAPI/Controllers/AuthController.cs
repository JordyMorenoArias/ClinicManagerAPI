using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        /// <param name="UserLogin">Login credentials.</param>
        /// <returns>JWT and user info if successful.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto UserLogin)
        {
            var result = await _authService.Login(UserLogin);

            Response.Cookies.Append("access_token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
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
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            var result = await _authService.Register(userRegister);
            return Ok(result);
        }
    }
}