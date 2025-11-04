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
        /// <param name="requesterRole"></param>
        /// <param name="addDoctorProfileDto"></param>
        /// <returns>> A task representing the asynchronous operation.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task<DoctorProfileDto> AddDoctorProfile(UserRole requesterRole, AddDoctorProfileDto addDoctorProfileDto);

        /// <summary>
        /// Deletes a doctor profile by its unique identifier.
        /// </summary>
        /// <param name="requesterRole"></param>
        /// <param name="doctorId"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task DeleteDoctorProfile(UserRole requesterRole, int doctorId);

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
        Task<PagedResult<DoctorProfileDto>> GetDoctorProfiles(QueryDoctorProfileParameters parameters);

        /// <summary>
        /// Updates an existing doctor profile.
        /// </summary>
        /// <param name="requesterRole"></param>
        /// <param name="updateDoctorProfileDto"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task<DoctorProfileDto> UpdateDoctorProfile(UserRole requesterRole, UpdateDoctorProfileDto updateDoctorProfileDto);
    }
}