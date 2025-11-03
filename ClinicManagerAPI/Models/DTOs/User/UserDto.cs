using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }


        public ICollection<AppointmentDto> CreatedAppointments { get; set; } = new List<AppointmentDto>();

        public ICollection<AppointmentDto> DoctorAppointments { get; set; } = new List<AppointmentDto>();

        public ICollection<MedicalRecordDto> MedicalRecords { get; set; } = new List<MedicalRecordDto>();

        public ICollection<DoctorProfileEntity> DoctorProfiles { get; set; } = new List<DoctorProfileEntity>();
    }
}
