using ClinicManagerAPI.Constants;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.PatientAllergy
{
    public class UpdatePatientAllergyDto
    {
        public SeverityAllergy Severity { get; set; } = SeverityAllergy.Mild;

        public string Notes { get; set; } = string.Empty;
    }
}
