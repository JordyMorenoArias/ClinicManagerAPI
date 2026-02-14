namespace ClinicManagerAPI.Models.DTOs.PatientAllergy
{
    public class PatientAllergyQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int? PatientId { get; set; }

        public int? AllergyId { get; set; }
    }
}