using ClinicManagerAPI.Models.DTOs.User;

namespace ClinicManagerAPI.Models.DTOs.Auth
{
    public class AuthResultDto
    {
        public string Token { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTimeOffset Expires { get; set; }

        public UserDto? User { get; set; }
    }
}
