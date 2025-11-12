using AutoMapper;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.Appointment.Interfaces;
using ClinicManagerAPI.Services.Patient.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;

namespace ClinicManagerAPI.Services.Appointment
{
    /// <summary>
    /// Service for managing appointment operations.
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IUserService userService, IPatientService patientService, IMapper mapper)
        {
            this._appointmentRepository = appointmentRepository;
            this._userService = userService;
            this._patientService = patientService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrieves an appointment by its unique identifier.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>A <see cref="AppointmentDto"/> object representing the appointment with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<AppointmentDto> GetAppointmentById(int appointmentId)
        {
            var appointmentEntity = await _appointmentRepository.GetAppointmentById(appointmentId);

            if (appointmentEntity == null)
                throw new KeyNotFoundException($"Appointment with ID {appointmentId} not found.");

            return _mapper.Map<AppointmentDto>(appointmentEntity);
        }

        /// <summary>
        /// Retrieves a paged list of appointments based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{AppointmentDto}"/> containing the paged list of appointments.</returns>
        public async Task<PagedResult<AppointmentDto>> GetAppointments(QueryAppointmentParameters parameters)
        {
            var appointments = await _appointmentRepository.GetAppointments(parameters);

            var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(appointments.Items);

            return new PagedResult<AppointmentDto>
            {
                Items = appointmentDtos,
                TotalItems = appointments.TotalItems,
                Page = appointments.Page,
                PageSize = appointments.PageSize
            };
        }

        /// <summary>
        /// Adds a new appointment.
        /// </summary>
        /// <param name="appointmentDto"></param>
        /// <returns>The created <see cref="AppointmentDto"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<AppointmentDto> AddAppointment(int requestId, AddAppointmentDto appointmentDto)
        {
            var patient = await _patientService.GetPatientById(appointmentDto.PatientId);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {appointmentDto.PatientId} not found.");

            var doctor = await _userService.GetUserById(appointmentDto.DoctorId);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID {appointmentDto.DoctorId} not found.");

            var appointmentEntity = _mapper.Map<Models.Entities.AppointmentEntity>(appointmentDto);
            appointmentEntity.CreatedById = requestId;
            var createdAppointment = await _appointmentRepository.AddAppointment(appointmentEntity);
            return _mapper.Map<AppointmentDto>(createdAppointment);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="appointmentDto"></param>
        /// <returns>The updated <see cref="AppointmentDto"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<AppointmentDto> UpdateAppointment(int requestId, UpdateAppointmentDto appointmentDto)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentById(appointmentDto.Id);

            if (existingAppointment == null)
                throw new KeyNotFoundException($"Appointment with ID {appointmentDto.Id} not found.");

            if (existingAppointment.PatientId != appointmentDto.PatientId)
            {
                var patient = await _patientService.GetPatientById(appointmentDto.PatientId);

                if (patient == null)
                    throw new KeyNotFoundException($"Patient with ID {appointmentDto.PatientId} not found.");
            }

            if (existingAppointment.DoctorId != appointmentDto.DoctorId)
            {
                var doctor = await _userService.GetUserById(appointmentDto.DoctorId);

                if (doctor == null)
                    throw new KeyNotFoundException($"Doctor with ID {appointmentDto.DoctorId} not found.");
            }

            _mapper.Map(appointmentDto, existingAppointment);
            existingAppointment.LastModifiedById = requestId;
            var updatedAppointment = await _appointmentRepository.UpdateAppointment(existingAppointment);
            return _mapper.Map<AppointmentDto>(updatedAppointment);
        }

        /// <summary>
        /// Deletes an appointment by its unique identifier.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>An <see cref="OperationResult"/> indicating the result of the deletion.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<OperationResult> DeleteAppointment(int appointmentId)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentById(appointmentId);

            if (existingAppointment == null)
                throw new KeyNotFoundException($"Appointment with ID {appointmentId} not found.");

            var success = await _appointmentRepository.DeleteAppointment(existingAppointment);

            return new OperationResult
            {
                Success = success,
                Message = "Appointment deleted successfully."
            };
        }
    }
}