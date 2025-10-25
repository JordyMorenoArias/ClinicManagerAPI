using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Repositories
{
    /// <summary>
    /// Repository for managing appointment data.
    /// </summary>
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicManagerContext _context;

        public AppointmentRepository(ClinicManagerContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Gets an appointment by its ID.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns> The appointment entity if found; otherwise, null.</returns>
        public async Task<AppointmentEntity?> GetAppointmentById(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        /// <summary>
        /// Gets a paged list of appointments based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Paged result of appointment entities.</returns>
        public async Task<PagedResult<AppointmentEntity>> GetAppointments(QueryAppointmentParameters parameters)
        {
            var query = _context.Appointments.AsQueryable();

            if (parameters.StartDateFilter.HasValue)
                query = query.Where(a => a.AppointmentDate >= parameters.StartDateFilter.Value);

            if (parameters.EndDateFilter.HasValue)
                query = query.Where(a => a.AppointmentDate <= parameters.EndDateFilter.Value);

            if (parameters.DoctorId.HasValue)
                query = query.Where(a => a.DoctorId == parameters.DoctorId.Value);

            if (parameters.PatientId.HasValue)
                query = query.Where(a => a.PatientId == parameters.PatientId.Value);

            if (parameters.Status.HasValue)
                query = query.Where(a => a.Status == parameters.Status.Value);

            query = parameters.SortBy switch
            {
                AppointmentSortBy.AppointmentDateAsc => query.OrderBy(a => a.AppointmentDate),
                AppointmentSortBy.AppointmentDateDesc => query.OrderByDescending(a => a.AppointmentDate),
                AppointmentSortBy.CreatedAtAsc => query.OrderBy(a => a.CreatedAt),
                AppointmentSortBy.CreatedAtDesc => query.OrderByDescending(a => a.CreatedAt),
                _ => query
            };

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<AppointmentEntity>
            {
                Items = items,
                TotalItems = totalItems,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }

        /// <summary>
        /// Adds a new appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        public async Task AddAppointment(AppointmentEntity appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        public async Task UpdateAppointment(AppointmentEntity appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        public async Task DeleteAppointment(AppointmentEntity appointment)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}