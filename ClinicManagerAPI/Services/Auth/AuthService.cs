using AutoMapper;
using ClinicManagerAPI.Models.DTOs;
using ClinicManagerAPI.Models.DTOs.Auth;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.Auth.Interfaces;
using ClinicManagerAPI.Services.Security.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClinicManagerAPI.Services.Auth
{
    /// <summary>
    /// Provides authentication services, including user login, registration, password management,
    /// and email verification.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="jwtService">The JWT service.</param>
        /// <param name="tokenGenerator">The token generator.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="passwordHasher">The password hasher.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public AuthService(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher<UserEntity> passwordHasher, IMapper mapper, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Logs in a user with the provided email and password.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <param name="password">User's password.</param>
        /// <returns>An authentication response containing a JWT token and user details.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the email or password is incorrect.</exception>
        public async Task<AuthResponseDto> Login(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsername(loginDto.UserName);

            if (user == null)
            {
                _logger.LogWarning("Login attempt failed: user not found for username {UserName}", loginDto.UserName);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                _logger.LogWarning("Login attempt failed: no password hash found for user {UserName}", loginDto.UserName);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Login attempt failed: invalid password for user {UserName}", loginDto.UserName);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var userTokenDto = _mapper.Map<UserGenerateTokenDto>(user);
            var token = _jwtService.GenerateJwtToken(userTokenDto);

            _logger.LogInformation("User {UserName} logged in successfully", loginDto.UserName);
           
            var userDto = _mapper.Map<UserDto>(user);
            return new AuthResponseDto
            {
                Token = token,
                Expires = DateTime.UtcNow.AddHours(3),
                User = userDto
            };
        }

        /// <summary>
        /// Registers the specified user register.
        /// </summary>
        /// <param name="userRegister">The user register.</param>
        /// <returns>A task representing the asynchronous operation, with an <see cref="OperationResult"/> indicating the result of the registration process.</returns>
        /// <exception cref="InvalidOperationException">User with this email already exists</exception>
        public async Task<OperationResult> Register(UserRegisterDto userRegister)
        {
            var user = await _userRepository.GetUserByEmail(userRegister.Email);

            if (user != null)
            {
                _logger.LogWarning("User with email {Email} already exists", userRegister.Email);
                throw new InvalidOperationException("User with this email already exists");
            }

            var passwordHash = _passwordHasher.HashPassword(new UserEntity(), userRegister.Password);
            var userEntity = _mapper.Map<UserEntity>(userRegister);
            userEntity.PasswordHash = passwordHash;
            var userResult = await _userRepository.AddUser(userEntity);

            _logger.LogInformation("User {Email} registered successfully", userResult.Email);
            return new OperationResult
            {
                Success = true,
                Message = "User registered successfully"
            };
        }
    }
}