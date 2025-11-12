using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.Entities
{
    public class DoctorProfileEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required, MaxLength(100)]
        public string Specialty { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public int? YearsOfExperience { get; set; }

        public string? LicenseNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserEntity Doctor { get; set; } = null!;
    }
}
