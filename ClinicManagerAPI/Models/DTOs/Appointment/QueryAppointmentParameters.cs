using ClinicManagerAPI.Constants;

namespace ClinicManagerAPI.Models.DTOs.Appointment
{
    public class QueryAppointmentParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTime? StartDateFilter { get; set; }

        public DateTime? EndDateFilter { get; set; }

        public int? DoctorId { get; set; }

        public int? PatientId { get; set; }

        public AppointmentStatus? Status { get; set; }

        public AppointmentSortBy SortBy { get; set; } = AppointmentSortBy.AppointmentDateAsc;
    }
}