namespace ClinicManagerAPI.Models.DTOs.Patient
{
    public class PatientQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTimeOffset? StartDateFilter { get; set; }

        public DateTimeOffset? EndDateFilter { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }

        public string? SearchTerm { get; set; }
    }
}