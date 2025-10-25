using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.Generic;

namespace ClinicManagerAPI.Services.Appointment.Interfaces
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Adds a new appointment.
        /// </summary>
        /// <param name="appointmentDto"></param>
        /// <returns>The created <see cref="AppointmentDto"/>.</returns>
        Task<AppointmentDto> AddAppointment(AddAppointmentDto appointmentDto);

        /// <summary>
        /// Deletes an appointment by its unique identifier.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>An <see cref="OperationResult"/> indicating the result of the deletion.</returns>
        Task<OperationResult> DeleteAppointment(int appointmentId);

        /// <summary>
        /// Retrieves an appointment by its unique identifier.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>A <see cref="AppointmentDto"/> object representing the appointment with the specified ID.</returns>
        Task<AppointmentDto> GetAppointmentById(int appointmentId);

        /// <summary>
        /// Retrieves a paged list of appointments based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{AppointmentDto}"/> containing the paged list of appointments.</returns>
        Task<PagedResult<AppointmentDto>> GetAppointments(QueryAppointmentParameters parameters);

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointmentDto"></param>
        /// <returns>The updated <see cref="AppointmentDto"/>.</returns>
        Task<AppointmentDto> UpdateAppointment(UpdateAppointmentDto appointmentDto);
    }
}