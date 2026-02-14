using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.DTOs.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        public PatientDto Patient { get; set; } = null!;

        [Required]
        public int? CreatedById { get; set; }  // Who recorded the appointment (Assistant o Doctor)
        public UserDto CreatedBy { get; set; } = null!;

        [Required]
        public int DoctorId { get; set; }
        public UserDto Doctor { get; set; } = null!;

        [Required]
        public DateTimeOffset Date { get; set; }

        [MaxLength(300)]
        public string Reason { get; set; } = string.Empty;

        [MaxLength(50)]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}