using ClinicManagerAPI.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.Entities
{
    public class AppointmentEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public PatientEntity Patient { get; set; } = null!;

        [Required, ForeignKey("User")]
        public int? CreatedById { get; set; }  // Who recorded the appointment (Assistant o Doctor)
        public UserEntity CreatedBy { get; set; } = null!;

        [ForeignKey("User")]
        public int? LastModifiedById { get; set; }
        public UserEntity? LastModifiedBy { get; set; }

        [Required, ForeignKey("User")]
        public int DoctorId { get; set; }
        public UserEntity Doctor { get; set; } = null!;

        [Required]
        public DateTimeOffset Date { get; set; }

        [MaxLength(300)]
        public string Reason { get; set; } = string.Empty;

        [MaxLength(50)]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}