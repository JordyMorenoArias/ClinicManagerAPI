using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.DTOs.User;

namespace ClinicManagerAPI.Models.DTOs.Report
{
    public class ReportSummaryDto
    {
        // Date range for the report
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        // Appointment statistics
        public int TotalAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int ConfirmedAppointments { get; set; }
        public int PendingAppointments { get; set; }
        public int CancelledAppointments { get; set; }

        // Patient statistics
        public int NewPatients { get; set; }
        public int ReturningPatients { get; set; }

        public IEnumerable<UserDto> TopDoctorsByCompletedAppointments { get; set; } = Enumerable.Empty<UserDto>();
        public IEnumerable<UserDto> TopAssistantsByScheduledAppointments { get; set; } = Enumerable.Empty<UserDto>();
        public IEnumerable<PatientDto> MostFrequentPatients { get; set; } = Enumerable.Empty<PatientDto>();
        public IEnumerable<TimeSlotReportDto> MostRequestedTimeSlots { get; set; } = Enumerable.Empty<TimeSlotReportDto>();
    }
}