using ClinicManagerAPI.Constants;

namespace ClinicManagerAPI.Models.DTOs.Appointment
{
    public class AppointmentQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTimeOffset? StartDateFilter { get; set; }

        public DateTimeOffset? EndDateFilter { get; set; }

        public int? DoctorId { get; set; }

        public int? PatientId { get; set; }

        public AppointmentStatus? Status { get; set; }

        public AppointmentSortBy SortBy { get; set; } = AppointmentSortBy.AppointmentDateAsc;
    }
}