namespace ClinicManagerAPI.Models.DTOs.User
{
    public class UserChangePasswordDto
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}