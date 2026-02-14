using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.Entities;
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

        public DateTimeOffset DateOfBirth { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ICollection<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
        public ICollection<MedicalRecordDto> MedicalRecords { get; set; } = new List<MedicalRecordDto>();
        public ICollection<PatientAllergyDto> Allergies { get; set; } = new List<PatientAllergyDto>();
    } 
}