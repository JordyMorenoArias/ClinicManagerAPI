using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.Patient
{
    public class PatientDto
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string Identification { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
        public ICollection<MedicalRecordDto> MedicalRecords { get; set; } = new List<MedicalRecordDto>();
    } 
}