namespace ClinicManagerAPI.Models.DTOs.Report
{
    public class ReportQueryParameters
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}