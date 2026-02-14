using AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.DTOs.Report;
using ClinicManagerAPI.Services.Appointment.Interfaces;
using ClinicManagerAPI.Services.Patient.Interfaces;
using ClinicManagerAPI.Services.Report.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;

namespace ClinicManagerAPI.Services.Report
{
    /// <summary>
    /// Provides services for generating reports based on appointments, patients, and users.
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportService"/> class.
        /// </summary>
        /// <param name="appointmentService"></param>
        /// <param name="userService"></param>
        /// <param name="patientService"></param>
        /// <param name="mapper"></param>
        public ReportService(IAppointmentService appointmentService, IUserService userService, IPatientService patientService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _patientService = patientService;
            _mapper = mapper;
        }

        /// <summary>
        /// Generates a comprehensive report based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="ReportSummaryDto"/> containing the report summary.</returns>
        public async Task<ReportSummaryDto> GenerateReportAsync(ReportQueryParameters parameters)
        {
            var appoinmentQuery = new AppointmentQueryParameters
            {
                StartDateFilter = parameters.StartDate,
                EndDateFilter = parameters.EndDate,
                PageSize = int.MaxValue
            };

            var appointments = await _appointmentService.GetAppointments(appoinmentQuery);

            var patientQuery = new PatientQueryParameters
            {
                StartDateFilter = parameters.StartDate,
                EndDateFilter = parameters.EndDate,
                PageSize = int.MaxValue
            };

            var patients = await _patientService.GetPatients(patientQuery);

            var userQuery = new Models.DTOs.User.UserQueryParameters
            {
                StartDateFilter = parameters.StartDate,
                EndDateFilter = parameters.EndDate,
                PageSize = int.MaxValue
            };

            int totalAppointments = appointments.TotalItems;
            int completedAppointments = appointments.Items.Count(a => a.Status == AppointmentStatus.Completed);
            int confirmedAppointments = appointments.Items.Count(a => a.Status == AppointmentStatus.Confirmed);
            int pendingAppointments = appointments.Items.Count(a => a.Status == AppointmentStatus.Pending);
            int cancelledAppointments = appointments.Items.Count(a => a.Status == AppointmentStatus.Cancelled);

            var newPatients = patients.Items.Count(p => p.CreatedAt >= parameters.StartDate && p.CreatedAt <= parameters.EndDate);
            var returningPatients = patients.Items.Count(p => p.CreatedAt < parameters.StartDate);

            var topDoctors = appointments.Items
                .Where(a => a.Status == AppointmentStatus.Completed)
                .GroupBy(a => a.Doctor)
                .Select(g => new
                {
                    Doctor = g.Key,
                    CompletedAppointments = g.Count()
                })
                .OrderByDescending(x => x.CompletedAppointments)
                .Take(5)
                .Select(x => x.Doctor)
                .ToList();

            var topAssistants = appointments.Items
                .Where(a => a.CreatedBy != null && a.CreatedBy.Role == UserRole.assistant)
                .GroupBy(a => a.CreatedBy)
                .Select(g => new
                {
                    Assistant = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .Select(x => x.Assistant)
                .ToList();

            var frequentPatients = appointments.Items
                .GroupBy(a => a.Patient)
                .Select(g => new
                {
                    Patient = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .Select(x => x.Patient)
                .ToList();

            var timeSlotStats = appointments.Items
                .GroupBy(a => a.Date.Hour)
                .Select(g => new TimeSlotReportDto
                {
                    TimeRange = $"{g.Key:00}:00 - {g.Key + 1:00}:00",
                    AppointmentCount = g.Count()
                })
                .OrderBy(x => x.TimeRange)
                .ToList();

            foreach (var patient in frequentPatients)
            {
                patient.Appointments = [];
                patient.MedicalRecords = [];
            }

            var report = new ReportSummaryDto
            {
                StartDate = parameters.StartDate,
                EndDate = parameters.EndDate,
                TotalAppointments = totalAppointments,
                CompletedAppointments = completedAppointments,
                ConfirmedAppointments = confirmedAppointments,
                PendingAppointments = pendingAppointments,
                CancelledAppointments = cancelledAppointments,
                NewPatients = newPatients,
                ReturningPatients = returningPatients,
                TopDoctorsByCompletedAppointments = topDoctors,
                TopAssistantsByScheduledAppointments = topAssistants,
                MostFrequentPatients = frequentPatients,
                MostRequestedTimeSlots = timeSlotStats
            };

            return report;
        }
    }
}