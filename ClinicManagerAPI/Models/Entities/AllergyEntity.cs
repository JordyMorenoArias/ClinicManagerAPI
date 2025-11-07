using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.Entities
{
    public class AllergyEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<PatientAllergyEntity> Patients { get; set; } = new List<PatientAllergyEntity>();
    }
}