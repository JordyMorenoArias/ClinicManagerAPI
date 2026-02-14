using ClinicManagerAPI.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagerAPI.Models.Entities
{
    public class PatientAllergyEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        public PatientEntity? Patient { get; set; }

        [Required]
        public int AllergyId { get; set; }
        public AllergyEntity? Allergy { get; set; }

        public SeverityAllergy Severity { get; set; } = SeverityAllergy.Mild;
        public DateTimeOffset DiagnosedAt { get; set; } = DateTimeOffset.UtcNow;
        public string Notes { get; set; } = string.Empty;
    }
}