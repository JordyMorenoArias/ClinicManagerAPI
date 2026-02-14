using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing appointment data.
    /// </summary>
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Adds a new appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> The added appointment entity.</returns>
        Task<AppointmentEntity> AddAppointment(AppointmentEntity appointment);

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> True if deletion was successful.</returns>
        Task<bool> DeleteAppointment(AppointmentEntity appointment);

        /// <summary>
        /// Gets an appointment by its ID.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns> The appointment entity if found; otherwise, null.</returns>
        Task<AppointmentEntity?> GetAppointmentById(int appointmentId);

        /// <summary>
        /// Gets a paged list of appointments based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Paged result of appointment entities.</returns>
        Task<PagedResult<AppointmentEntity>> GetAppointments(AppointmentQueryParameters parameters);

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> The updated appointment entity.</returns>
        Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment);
    }
}