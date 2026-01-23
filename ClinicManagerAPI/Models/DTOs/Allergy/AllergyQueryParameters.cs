namespace ClinicManagerAPI.Models.DTOs.Allergy
{
    public class AllergyQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SearchTerm { get; set; }
    }
}