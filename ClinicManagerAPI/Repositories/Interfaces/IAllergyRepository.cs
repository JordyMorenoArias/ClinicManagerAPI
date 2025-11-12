using ClinicManagerAPI.Models.DTOs.Allergy;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing Allergy entities.
    /// </summary>
    public interface IAllergyRepository
    {

        /// <summary>
        /// Create a new allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns> The created allergy entity.</returns>
        Task<AllergyEntity> CreateAllergy(AllergyEntity allergy);

        /// <summary>
        /// Delete an allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns> True if deletion was successful.</returns>
        Task<bool> DeleteAllergy(AllergyEntity allergy);

        /// <summary>
        /// Get a paginated list of allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A paginated result of allergy entities.</returns>
        Task<PagedResult<AllergyEntity>> GetAllergies(QueryAllergyParameters parameters);

        /// <summary>
        /// Get an allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The allergy entity if found; otherwise, null.</returns>
        Task<AllergyEntity?> GetAllergyById(int id);


        /// <summary>
        /// Update an existing allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns> The updated allergy entity.</returns>
        Task<AllergyEntity> UpdateAllergy(AllergyEntity allergy);
    }
}