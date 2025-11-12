using ClinicManagerAPI.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.User
{
    public class UserRegisterDto
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        [Required, PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
