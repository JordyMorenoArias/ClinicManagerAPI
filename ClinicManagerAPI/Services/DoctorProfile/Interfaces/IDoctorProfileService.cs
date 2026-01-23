using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Models.DTOs.Generic;

namespace ClinicManagerAPI.Services.DoctorProfile.Interfaces
{
    /// <summary>
    /// Provides operations related to doctor profile management.
    /// </summary>
    public interface IDoctorProfileService
    {
        /// <summary>
        /// Adds a new doctor profile.
        /// </summary>
        /// <param name="addDoctorProfileDto"></param>
        /// <returns>> A task representing the asynchronous operation.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task<DoctorProfileDto> AddDoctorProfile(AddDoctorProfileDto addDoctorProfileDto);

        /// <summary>
        /// Deletes a doctor profile by its unique identifier.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task DeleteDoctorProfile(int doctorId);

        /// <summary>
        /// Retrieves a doctor profile by the doctor's unique identifier.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A <see cref="DoctorProfileDto"/> object representing the doctor profile with the specified ID.</returns>
        Task<DoctorProfileDto> GetDoctorProfileById(int doctorId);

        /// <summary>
        /// Retrieves a paged list of doctor profiles based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A <see cref="PagedResult{DoctorProfileDto}"/> containing the paged list of doctor profiles.</returns>
        Task<PagedResult<DoctorProfileDto>> GetDoctorProfiles(DoctorProfileQueryParameters parameters);

        /// <summary>
        /// Updates an existing doctor profile.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDoctorProfileDto"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<DoctorProfileDto> UpdateDoctorProfile(int id, UpdateDoctorProfileDto updateDoctorProfileDto);
    }
}