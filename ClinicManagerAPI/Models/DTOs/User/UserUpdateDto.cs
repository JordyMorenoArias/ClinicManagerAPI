using ClinicManagerAPI.Constants;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.User
{
    public class UserUpdateDto
    {

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
