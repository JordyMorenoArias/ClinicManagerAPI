using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Repositories
{
    /// <summary>
    /// Repository for managing doctor profile data.
    /// </summary>
    public class DoctorProfileRepository : IDoctorProfileRepository
    {
        private readonly ClinicManagerContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorProfileRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public DoctorProfileRepository(ClinicManagerContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retrieves a doctor profile by the doctor's unique identifier.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A <see cref="DoctorProfileEntity"/> object representing the doctor profile with the specified ID, or <c>null</c> if no matching profile is found.</returns>
        public async Task<DoctorProfileEntity?> GetDoctorProfileById(int doctorId)
        {
            return await context.DoctorProfiles.FindAsync(doctorId);
        }

        /// <summary>
        /// Retrieves a paged list of doctor profiles based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A <see cref="PagedResult{DoctorProfileEntity}"/> containing the paged list of doctor profiles.</returns>
        public async Task<PagedResult<DoctorProfileEntity>> GetDoctorProfiles(DoctorProfileQueryParameters parameters)
        {
            var query = context.DoctorProfiles.AsQueryable();

            if (parameters.DoctorId.HasValue)
            {
                query = query.Where(dp => dp.DoctorId == parameters.DoctorId);
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(dp => dp.Doctor)
                .ToListAsync();

            return new PagedResult<DoctorProfileEntity>
            {
                Page = parameters.Page,
                PageSize = parameters.PageSize,
                TotalItems = totalItems,
                Items = items
            };
        }

        /// <summary>
        /// Adds a new doctor profile to the database.
        /// </summary>
        /// <param name="doctorProfile"></param>
        /// <returns> the added <see cref="DoctorProfileEntity"/>.</returns>
        public async Task<DoctorProfileEntity> AddDoctorProfile(DoctorProfileEntity doctorProfile)
        {
            await context.DoctorProfiles.AddAsync(doctorProfile);
            await context.SaveChangesAsync();
            return doctorProfile;
        }

        /// <summary>
        /// Updates an existing doctor profile in the database.
        /// </summary>
        /// <param name="doctorProfile"></param>
        /// <returns> the updated <see cref="DoctorProfileEntity"/>.</returns>
        public async Task<DoctorProfileEntity> UpdateDoctorProfile(DoctorProfileEntity doctorProfile)
        {
            context.DoctorProfiles.Update(doctorProfile);
            await context.SaveChangesAsync();
            return doctorProfile;
        }

        /// <summary>
        /// Deletes a doctor profile from the database.
        /// </summary>
        /// <param name="doctorProfile"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        public async Task DeleteDoctorProfile(DoctorProfileEntity doctorProfile)
        {
            context.DoctorProfiles.Remove(doctorProfile);
            await context.SaveChangesAsync();
        }
    }
}