using ClinicManagerAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.Allergy
{
    public class AllergyDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<PatientAllergyDto> Patients { get; set; } = new List<PatientAllergyDto>();
    }
}