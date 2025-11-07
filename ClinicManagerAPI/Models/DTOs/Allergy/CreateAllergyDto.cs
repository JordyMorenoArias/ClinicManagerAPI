using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.Allergy
{
    public class CreateAllergyDto
    {

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;
    }
}
