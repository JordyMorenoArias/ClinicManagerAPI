using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;

namespace ClinicManagerAPI.Services.MedicalRecord.Interfaces
{
    public interface IMedicalRecordService
    {
        /// <summary>
        /// Adds a new medical record.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="requestRole"></param>
        /// <param name="medicalRecordDto"></param>
        /// <returns>The added <see cref="MedicalRecordDto"/>.</returns>
        Task<MedicalRecordDto> AddMedicalRecord(int requestId, UserRole requestRole, AddMedicalRecordDto medicalRecordDto);

        /// <summary>
        /// Deletes a medical record.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="requestRole"></param>
        /// <param name="medicalRecordId"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteMedicalRecord(int requestId, UserRole requestRole, int medicalRecordId);

        /// <summary>
        /// Retrieves a medical record by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="MedicalRecordDto"/> object representing the medical record with the specified ID.</returns>
        Task<MedicalRecordDto> GetMedicalRecordById(int id);

        /// <summary>
        /// Retrieves a paged list of medical records based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{MedicalRecordDto}"/> containing the paged list of medical records.</returns>
        Task<PagedResult<MedicalRecordDto>> GetMedicalRecords(QueryMedicalRecordParameters parameters);

        /// <summary>
        /// Updates an existing medical record.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="requestRole"></param>
        /// <param name="medicalRecordDto"></param>
        /// <returns>The updated <see cref="MedicalRecordDto"/>.</returns>
        Task<MedicalRecordDto> UpdateMedicalRecord(int requestId, UserRole requestRole, UpdateMedicalRecordDto medicalRecordDto);
    }
}