using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Allergy;
using ClinicManagerAPI.Models.DTOs.Patient;

namespace ClinicManagerAPI.Models.Entities
{
    public class PatientAllergyDto
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public PatientDto? Patient { get; set; }

        public int AllergyId { get; set; }
        public AllergyDto? Allergy { get; set; }

        public SeverityAllergy Severity { get; set; } = SeverityAllergy.Mild;
        public DateTimeOffset DiagnosedAt { get; set; } = DateTimeOffset.UtcNow;
        public string Notes { get; set; } = string.Empty;
    }
}