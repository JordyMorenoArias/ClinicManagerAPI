using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.PatientAllergy;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Services.PatientAllergy.Interfaces
{
    /// <summary>
    /// Service for managing patient allergies.
    /// </summary>
    public interface IPatientAllergyService
    {
        /// <summary>
        /// Add a new patient allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="addDto"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the created PatientAllergyDto.</returns>
        Task<PatientAllergyDto> AddPatientAllergy(UserRole requestRole, AddPatientAllergyDto addDto);

        /// <summary>
        /// Delete a patient allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <returns> A task that represents the asynchronous operation.</returns>
        Task DeletePatientAllergy(UserRole requestRole, int id);

        /// <summary>
        /// Get a paginated list of patient allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the PagedResult of PatientAllergyDto.</returns>
        Task<PagedResult<PatientAllergyDto>> GetPatientAllergies(QueryPatientAllergyParameters parameters);

        /// <summary>
        /// Get a patient allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the PatientAllergyDto.</returns>
        Task<PatientAllergyDto> GetPatientAllergyById(int id);

        /// <summary>
        /// Update an existing patient allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the updated PatientAllergyDto.</returns>
        Task<PatientAllergyDto> UpdatePatientAllergy(UserRole requestRole, int id, UpdatePatientAllergyDto updateDto);
    }
}