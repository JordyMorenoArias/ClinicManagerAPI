using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.DTOs.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.DTOs.Appointment
{
    public class AddAppointmentDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(300)]
        public string Reason { get; set; } = string.Empty;
    }
}
