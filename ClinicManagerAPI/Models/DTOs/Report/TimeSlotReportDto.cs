namespace ClinicManagerAPI.Models.DTOs.Report
{
    public class TimeSlotReportDto
    {
        public string TimeRange { get; set; } = string.Empty;

        public int AppointmentCount { get; set; }
    }
}