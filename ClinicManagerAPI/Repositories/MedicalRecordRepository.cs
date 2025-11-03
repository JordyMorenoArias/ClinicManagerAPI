using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Repositories
{
    /// <summary>
    /// Repository for managing medical record data.
    /// </summary>
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ClinicManagerContext _context;

        public MedicalRecordRepository(ClinicManagerContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Retrieves a medical record by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="MedicalRecordEntity"/> object representing the medical record with the specified ID, or <c>null</c> if no matching record is found.</returns>
        public async Task<MedicalRecordEntity?> GetMedicalRecordById(int id)
        {
            return await _context.MedicalRecords.FindAsync(id);
        }

        /// <summary>
        /// Retrieves a paged list of medical records based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{MedicalRecordEntity}"/> containing the paged list of medical records.</returns>
        public async Task<PagedResult<MedicalRecordEntity>> GetMedicalRecords(QueryMedicalRecordParameters parameters)
        {
            var query = _context.MedicalRecords.AsNoTracking();

            if (parameters.patientId.HasValue)
                query = query.Where(mr => mr.PatientId == parameters.patientId.Value);

            if (parameters.doctorId.HasValue)
                query = query.Where(mr => mr.DoctorId == parameters.doctorId.Value);

            var totalItems = await query.CountAsync();
            var medicalRecords = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .ToListAsync();

            return new PagedResult<MedicalRecordEntity>
            {
                Items = medicalRecords,
                TotalItems = totalItems,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }

        /// <summary>
        /// Adds a new medical record to the database.
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddMedicalRecord(MedicalRecordEntity medicalRecord)
        {
            await _context.MedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing medical record in the database.
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateMedicalRecord(MedicalRecordEntity medicalRecord)
        {
            _context.MedicalRecords.Update(medicalRecord);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a medical record from the database.
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteMedicalRecord(MedicalRecordEntity medicalRecord)
        {
            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();
        }
    }
}