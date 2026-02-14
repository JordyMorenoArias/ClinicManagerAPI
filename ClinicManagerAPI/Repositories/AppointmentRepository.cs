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
        public async Task<PagedResult<AppointmentEntity>> GetAppointments(AppointmentQueryParameters parameters)
        {
            var query = _context.Appointments.AsNoTracking().AsQueryable();

            if (parameters.StartDateFilter.HasValue)
                query = query.Where(a => a.Date >= parameters.StartDateFilter.Value);

            if (parameters.EndDateFilter.HasValue)
                query = query.Where(a => a.Date <= parameters.EndDateFilter.Value);

            if (parameters.DoctorId.HasValue)
                query = query.Where(a => a.DoctorId == parameters.DoctorId.Value);

            if (parameters.PatientId.HasValue)
                query = query.Where(a => a.PatientId == parameters.PatientId.Value);

            if (parameters.Status.HasValue)
                query = query.Where(a => a.Status == parameters.Status.Value);

            query = parameters.SortBy switch
            {
                AppointmentSortBy.AppointmentDateAsc => query.OrderBy(a => a.Date),
                AppointmentSortBy.AppointmentDateDesc => query.OrderByDescending(a => a.Date),
                AppointmentSortBy.CreatedAtAsc => query.OrderBy(a => a.CreatedAt),
                AppointmentSortBy.CreatedAtDesc => query.OrderByDescending(a => a.CreatedAt),
                _ => query
            };

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(a => new AppointmentEntity
                {
                    Id = a.Id,
                    PatientId = a.PatientId,
                    Patient = new PatientEntity
                    {
                        Id = a.Patient.Id,
                        FullName = a.Patient.FullName,
                        Identification = a.Patient.Identification,
                        Phone = a.Patient.Phone,
                        Email = a.Patient.Email,
                        Address = a.Patient.Address,
                        DateOfBirth = a.Patient.DateOfBirth,
                        CreatedAt = a.Patient.CreatedAt
                    },
                    DoctorId = a.DoctorId,
                    Doctor = new UserEntity
                    {
                        Id = a.Doctor.Id,
                        FullName = a.Doctor.FullName,
                        Username = a.Doctor.Username,
                        Email = a.Doctor.Email,
                        PhoneNumber = a.Doctor.PhoneNumber,
                        Role = a.Doctor.Role,       
                        CreatedAt = a.Doctor.CreatedAt
                    },
                    Date = a.Date,
                    Reason = a.Reason,
                    Status = a.Status,
                    CreatedAt = a.CreatedAt
                })
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
        /// <returns> The added appointment entity.</returns>
        public async Task<AppointmentEntity> AddAppointment(AppointmentEntity appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> The updated appointment entity.</returns>
        public async Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns> True if deletion was successful.</returns>
        public async Task<bool> DeleteAppointment(AppointmentEntity appointment)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}