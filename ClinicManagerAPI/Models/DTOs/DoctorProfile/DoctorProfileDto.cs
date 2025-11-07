using ClinicManagerAPI.Models.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.DoctorProfile
{
    public class DoctorProfileDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required, MaxLength(100)]
        public string Specialty { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public int? YearsOfExperience { get; set; }

        public string? LicenseNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserDto Doctor { get; set; } = null!;
    }
}
