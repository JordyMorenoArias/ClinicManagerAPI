using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Repositories
{
    /// <summary>
    /// Repository for managing patient data.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicManagerContext _context;

        public PatientRepository(ClinicManagerContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Retrieves a patient by their unique identifier.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>A <see cref="PatientEntity"/> object representing the patient with the specified ID, or <c>null</c> if no matching patient is found.</returns>
        public async Task<PatientEntity?> GetPatientById(int patientId)
        {
            return await _context.Patients.FindAsync(patientId);
        }

        /// <summary>
        /// Retrieves a patient by their identification number.
        /// </summary>
        /// <param name="identification"></param>
        /// <returns>A <see cref="PatientEntity"/> object representing the patient with the specified identification number, or <c>null</c> if no matching patient is found.</returns>
        public async Task<PatientEntity?> GetPatientByIdentification(string identification)
        {
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Identification == identification);
        }

        /// <summary>
        /// Retrieves a paged list of patients based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{PatientEntity}"/> containing the paged list of patients.</returns>
        public async Task<PagedResult<PatientEntity>> GetPatientsPagedAsync(QueryPatientParameters parameters)
        {
            var query = _context.Patients.AsNoTracking();

            if (parameters.DateOfBirth.HasValue)
            {
                query = query.Where(p => p.DateOfBirth.Date == parameters.DateOfBirth.Value.Date);
            }

            var totalItems = await query.CountAsync();

            var patients = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<PatientEntity>
            {
                Items = patients,
                TotalItems = totalItems,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }

        /// <summary>
        /// Adds a new patient to the database.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<PatientEntity> AddPatient(PatientEntity patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        /// <summary>
        /// Updates an existing patient's information in the database.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<PatientEntity> UpdatePatient(PatientEntity patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        /// <summary>
        /// Deletes a patient from the database.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeletePatient(PatientEntity patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}