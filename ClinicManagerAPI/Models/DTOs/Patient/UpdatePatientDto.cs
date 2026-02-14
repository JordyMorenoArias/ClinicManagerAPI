using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.Patient
{
    public class UpdatePatientDto
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string Identification { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
