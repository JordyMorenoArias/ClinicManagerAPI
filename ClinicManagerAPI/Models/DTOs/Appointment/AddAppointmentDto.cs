using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.Appointment
{
    public class AddAppointmentDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [MaxLength(300)]
        public string Reason { get; set; } = string.Empty;
    }
}
