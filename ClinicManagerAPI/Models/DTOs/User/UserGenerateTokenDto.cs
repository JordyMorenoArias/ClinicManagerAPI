using ClinicManagerAPI.Constants;

namespace ClinicManagerAPI.Models.DTOs
{
    public class UserGenerateTokenDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
