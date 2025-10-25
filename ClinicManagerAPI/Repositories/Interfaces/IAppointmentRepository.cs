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
        /// <returns> A task representing the asynchronous operation.</returns>
        Task AddAppointment(AppointmentEntity appointment);

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task DeleteAppointment(AppointmentEntity appointment);

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
        Task<PagedResult<AppointmentEntity>> GetAppointments(QueryAppointmentParameters parameters);

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task UpdateAppointment(AppointmentEntity appointment);
    }
}