using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.PatientAllergy;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing PatientAllergy entities.
    /// </summary>
    public interface IPatientAllergyRepository
    {
        /// <summary>
        /// Create a new PatientAllergy
        /// </summary>
        /// <param name="patientAllergy"></param>
        /// <returns> The created PatientAllergy entity </returns>
        Task<PatientAllergyEntity> CreatePatientAllergy(PatientAllergyEntity patientAllergy);

        /// <summary>
        /// Delete a PatientAllergy
        /// </summary>
        /// <param name="patientAllergy"></param>
        /// <returns> True if deletion was successful </returns>
        Task<bool> DeletePatientAllergy(PatientAllergyEntity patientAllergy);

        /// <summary>
        /// Get a paginated list of PatientAllergies based on query parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A paginated result of PatientAllergy entities </returns>
        Task<PagedResult<PatientAllergyEntity>> GetPatientAllergies(QueryPatientAllergyParameters parameters);

        /// <summary>
        /// Get a PatientAllergy by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The PatientAllergy entity if found, otherwise null </returns>
        Task<PatientAllergyEntity?> GetPatientAllergyById(int id);

        /// <summary>
        /// Update an existing PatientAllergy
        /// </summary>
        /// <param name="patientAllergy"></param>
        /// <returns> The updated PatientAllergy entity if successful, otherwise null </returns>
        Task<PatientAllergyEntity?> UpdatePatientAllergy(PatientAllergyEntity patientAllergy);
    }
}