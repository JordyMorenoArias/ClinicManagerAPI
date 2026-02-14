using ClinicManagerAPI.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [InverseProperty("CreatedBy")]
        public ICollection<AppointmentEntity> CreatedAppointments { get; set; } = new List<AppointmentEntity>();

        [InverseProperty("Doctor")]
        public ICollection<AppointmentEntity> DoctorAppointments { get; set; } = new List<AppointmentEntity>();

        public ICollection<MedicalRecordEntity> MedicalRecords { get; set; } = new List<MedicalRecordEntity>();

        [InverseProperty("Doctor")]
        public ICollection<DoctorProfileEntity> DoctorProfiles { get; set; } = new List<DoctorProfileEntity>();
    }
}