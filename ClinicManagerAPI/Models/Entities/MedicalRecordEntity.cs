using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.Entities
{
    public class MedicalRecordEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public int PatientId { get; set; }
        public PatientEntity Patient { get; set; } = new PatientEntity();

        [Required, ForeignKey("User")]
        public int DoctorId { get; set; }
        public UserEntity Doctor { get; set; } = null!;

        [Required, ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public AppointmentEntity Appointment { get; set; } = null!;

        [Required, MaxLength(500)]
        public string Diagnosis { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Treatment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}