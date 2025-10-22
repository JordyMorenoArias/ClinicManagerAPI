namespace ClinicManagerAPI.Models.DTOs.Patient
{
    public class QueryPatientParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTime? DateOfBirth { get; set; }

    }
}
