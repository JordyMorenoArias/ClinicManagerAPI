using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.PatientAllergy
{
    public class AddPatientAllergyDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int AllergyId { get; set; }

        public SeverityAllergy Severity { get; set; } = SeverityAllergy.Mild;

        public DateTime DiagnosedAt { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; } = string.Empty;
    }
}