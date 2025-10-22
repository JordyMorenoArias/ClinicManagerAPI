using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.Patient;

namespace ClinicManagerAPI.Services.Patient.Interfaces
{
    public interface IPatientService
    {
        /// <summary>
        /// Adds a new patient to the system.
        /// </summary>
        /// <param name="addPatientDto"></param>
        /// <returns>A <see cref="PatientDto"/> representing the newly added patient.</returns>
        Task<PatientDto> AddPatient(AddPatientDto addPatientDto);

        /// <summary>
        /// Retrieves a patient by their unique identifier.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>A <see cref="PatientDto"/> representing the patient with the specified ID.</returns>
        Task<PatientDto> GetPatientById(int patientId);

        /// <summary>
        /// Retrieves a patient by their identification number.
        /// </summary>
        /// <param name="identification"></param>
        /// <returns>A <see cref="PatientDto"/> representing the patient with the specified identification number.</returns>
        Task<PatientDto> GetPatientByIdentification(string identification);

        /// <summary>
        /// Retrieves a paged list of patients based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{PatientDto}"/> containing the paged list of patients.</returns>
        Task<PagedResult<PatientDto>> GetPatientsPagedAsync(QueryPatientParameters parameters);

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="updatePatientDto"></param>
        /// <returns>A <see cref="PatientDto"/> representing the updated patient.</returns>
        Task<PatientDto> UpdatePatient(UpdatePatientDto updatePatientDto);
    }
}