using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.DoctorProfile
{
    public class UpdateDoctorProfileDto
    {
        [Required, MaxLength(100)]
        public string Specialty { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public int? YearsOfExperience { get; set; }

        public string? LicenseNumber { get; set; }
    }
}