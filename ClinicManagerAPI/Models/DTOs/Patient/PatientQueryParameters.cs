namespace ClinicManagerAPI.Models.DTOs.Patient
{
    public class PatientQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTime? StartDateFilter { get; set; }

        public DateTime? EndDateFilter { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? SearchTerm { get; set; }
    }
}