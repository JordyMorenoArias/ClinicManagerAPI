using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.MedicalRecord
{
    public class UpdateMedicalRecordDto
    {
        [Required, MaxLength(500)]
        public string Diagnosis { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Treatment { get; set; } = string.Empty;
    }
}
