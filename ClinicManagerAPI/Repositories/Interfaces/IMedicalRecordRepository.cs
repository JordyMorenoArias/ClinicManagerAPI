using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.Entities;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing medical record data.
    /// </summary>
    public interface IMedicalRecordRepository
    {
        /// <summary>
        /// Adds a new medical record to the database.
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddMedicalRecord(MedicalRecordEntity medicalRecord);

        /// <summary>
        /// Deletes a medical record from the database.
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteMedicalRecord(MedicalRecordEntity medicalRecord);

        /// <summary>
        /// Retrieves a medical record by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="MedicalRecordEntity"/> object representing the medical record with the specified ID, or <c>null</c> if no matching record is found.</returns>
        Task<MedicalRecordEntity?> GetMedicalRecordById(int id);

        /// <summary>
        /// Retrieves a paged list of medical records based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{MedicalRecordEntity}"/> containing the paged list of medical records.</returns>
        Task<PagedResult<MedicalRecordEntity>> GetMedicalRecords(QueryMedicalRecordParameters parameters);

        /// <summary>
        /// Updates an existing medical record in the database.
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateMedicalRecord(MedicalRecordEntity medicalRecord);
    }
}