using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "UserName is required"), EmailAddress]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "password is required"), PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
