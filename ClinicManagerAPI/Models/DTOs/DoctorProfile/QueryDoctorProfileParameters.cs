namespace ClinicManagerAPI.Models.DTOs.DoctorProfile
{
    public class QueryDoctorProfileParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int? DoctorId { get; set; }
    }
}