using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing patient data.
    /// </summary>
    public interface IPatientRepository
    {
        /// <summary>
        /// Adds a new patient to the database.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<PatientEntity> AddPatient(PatientEntity patient);

        /// <summary>
        /// Deletes a patient from the database.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeletePatient(PatientEntity patient);

        /// <summary>
        /// Retrieves a patient by their unique identifier.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>A <see cref="PatientEntity"/> object representing the patient with the specified ID, or <c>null</c> if no matching patient is found.</returns>
        Task<PatientEntity?> GetPatientById(int patientId);


        /// <summary>
        /// Retrieves a patient by their identification number.
        /// </summary>
        /// <param name="identification"></param>
        /// <returns>A <see cref="PatientEntity"/> object representing the patient with the specified identification number, or <c>null</c> if no matching patient is found.</returns>
        Task<PatientEntity?> GetPatientByIdentification(string identification);

        /// <summary>
        /// Retrieves a paged list of patients based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{PatientEntity}"/> containing the paged list of patients.</returns>
        Task<PagedResult<PatientEntity>> GetPatients(PatientQueryParameters parameters);

        /// <summary>
        /// Updates an existing patient's information in the database.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<PatientEntity> UpdatePatient(PatientEntity patient);
    }
}