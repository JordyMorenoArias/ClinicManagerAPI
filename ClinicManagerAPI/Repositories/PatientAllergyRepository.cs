using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.PatientAllergy;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Repositories
{
    /// <summary>
    /// Repository for managing PatientAllergy entities.
    /// </summary>
    public class PatientAllergyRepository : IPatientAllergyRepository
    {
        private readonly ClinicManagerContext _context;

        /// <summary>
        /// Constructor for PatientAllergyRepository
        /// </summary>
        /// <param name="context"></param>
        public PatientAllergyRepository(ClinicManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a PatientAllergy by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The PatientAllergy entity if found, otherwise null </returns>
        public async Task<PatientAllergyEntity?> GetPatientAllergyById(int id)
        {
            return await _context.PatientAllergies.FindAsync(id);
        }

        /// <summary>
        /// Get a paginated list of PatientAllergies based on query parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A paginated result of PatientAllergy entities </returns>
        public async Task<PagedResult<PatientAllergyEntity>> GetPatientAllergies(QueryPatientAllergyParameters parameters)
        {
            var query = _context.PatientAllergies.AsQueryable();

            if (parameters.PatientId.HasValue)
                query = query.Where(pa => pa.PatientId == parameters.PatientId.Value);

            if (parameters.AllergyId.HasValue)
                query = query.Where(pa => pa.AllergyId == parameters.AllergyId.Value);

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(pa => pa.Patient)
                .Include(pa => pa.Allergy)
                .ToListAsync();

            return new PagedResult<PatientAllergyEntity>
            {
                Items = items,
                TotalItems = totalItems,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }

        /// <summary>
        /// Create a new PatientAllergy
        /// </summary>
        /// <param name="patientAllergy"></param>
        /// <returns> The created PatientAllergy entity </returns>
        public async Task<PatientAllergyEntity> CreatePatientAllergy(PatientAllergyEntity patientAllergy)
        {
            _context.PatientAllergies.Add(patientAllergy);
            await _context.SaveChangesAsync();
            return patientAllergy;
        }

        /// <summary>
        /// Update an existing PatientAllergy
        /// </summary>
        /// <param name="patientAllergy"></param>
        /// <returns> The updated PatientAllergy entity if successful, otherwise null </returns>
        public async Task<PatientAllergyEntity?> UpdatePatientAllergy(PatientAllergyEntity patientAllergy)
        {
            _context.PatientAllergies.Update(patientAllergy);
            await _context.SaveChangesAsync();
            return patientAllergy;
        }

        /// <summary>
        /// Delete a PatientAllergy
        /// </summary>
        /// <param name="patientAllergy"></param>
        /// <returns> True if deletion was successful </returns>
        public async Task<bool> DeletePatientAllergy(PatientAllergyEntity patientAllergy)
        {
            _context.PatientAllergies.Remove(patientAllergy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}