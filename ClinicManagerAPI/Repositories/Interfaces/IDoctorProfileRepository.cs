using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing doctor profile data.
    /// </summary>
    public interface IDoctorProfileRepository
    {
        /// <summary>
        /// Adds a new doctor profile to the database.
        /// </summary>
        /// <param name="doctorProfile"></param>
        /// <returns> the added <see cref="DoctorProfileEntity"/>.</returns>
        Task<DoctorProfileEntity> AddDoctorProfile(DoctorProfileEntity doctorProfile);

        /// <summary>
        /// Deletes a doctor profile from the database.
        /// </summary>
        /// <param name="doctorProfile"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task DeleteDoctorProfile(DoctorProfileEntity doctorProfile);

        /// <summary>
        /// Retrieves a doctor profile by the doctor's unique identifier.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A <see cref="DoctorProfileEntity"/> object representing the doctor profile with the specified ID, or <c>null</c> if no matching profile is found.</returns>
        Task<DoctorProfileEntity?> GetDoctorProfileById(int doctorId);

        /// <summary>
        /// Retrieves a paged list of doctor profiles based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A <see cref="PagedResult{DoctorProfileEntity}"/> containing the paged list of doctor profiles.</returns>
        Task<PagedResult<DoctorProfileEntity>> GetDoctorProfiles(DoctorProfileQueryParameters parameters);

        /// <summary>
        /// Updates an existing doctor profile in the database.
        /// </summary>
        /// <param name="doctorProfile"></param>
        /// <returns> the updated <see cref="DoctorProfileEntity"/>.</returns>
        Task<DoctorProfileEntity> UpdateDoctorProfile(DoctorProfileEntity doctorProfile);
    }
}