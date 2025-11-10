using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Allergy;
using ClinicManagerAPI.Models.DTOs.Generic;

namespace ClinicManagerAPI.Services.Allergy.Interfaces
{
    /// <summary>
    /// Service for managing allergies.
    /// </summary>
    public interface IAllergyService
    {
        /// <summary>
        /// Create a new allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="createAllergyDto"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the created AllergyDto.</returns>
        Task<AllergyDto> CreateAllergy(UserRole requestRole, CreateAllergyDto createAllergyDto);

        /// <summary>
        /// Delete an allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <returns>An <see cref="OperationResult"/> indicating the result of the deletion.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<OperationResult> DeleteAllergy(UserRole requestRole, int id);

        /// <summary>
        /// Get a paginated list of allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the PagedResult of AllergyDto.</returns>
        Task<PagedResult<AllergyDto>> GetAllergies(QueryAllergyParameters parameters);

        /// <summary>
        /// Get an allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the AllergyDto.</returns>
        Task<AllergyDto> GetAllergyById(int id);

        /// <summary>
        /// Update an existing allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <param name="updateAllergyDto"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the updated AllergyDto.</returns>
        Task<AllergyDto> UpdateAllergy(UserRole requestRole, int id, UpdateAllergyDto updateAllergyDto);
    }
}