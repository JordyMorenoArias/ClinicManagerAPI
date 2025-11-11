using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models.DTOs.Allergy;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Repositories
{
    /// <summary>
    /// Repository for managing Allergy entities.
    /// </summary>
    public class AllergyRepository : IAllergyRepository
    {
        private readonly ClinicManagerContext _context;

        public AllergyRepository(ClinicManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get an allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> The allergy entity if found; otherwise, null.</returns>
        public async Task<AllergyEntity?> GetAllergyById(int id)
        {
            return await _context.Allergies.FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Get a paginated list of allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A paginated result of allergy entities.</returns>
        public async Task<PagedResult<AllergyEntity>> GetAllergies(QueryAllergyParameters parameters)
        {
            var query = _context.Allergies.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                var filter = parameters.SearchTerm.Trim().ToLower();
                query = query.Where(a => a.Name.ToLower().Contains(filter));
            }

            var totalItems = await query.CountAsync();
            var allergies = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResult<AllergyEntity>
            {
                Items = allergies,
                TotalItems = totalItems,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }

        /// <summary>
        /// Create a new allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns> The created allergy entity.</returns>
        public async Task<AllergyEntity> CreateAllergy(AllergyEntity allergy)
        {
            _context.Allergies.Add(allergy);
            await _context.SaveChangesAsync();
            return allergy;
        }

        /// <summary>
        /// Update an existing allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns> The updated allergy entity.</returns>
        public async Task<AllergyEntity> UpdateAllergy(AllergyEntity allergy)
        {
            _context.Allergies.Update(allergy);
            await _context.SaveChangesAsync();
            return allergy;
        }

        /// <summary>
        /// Delete an allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns> True if deletion was successful.</returns>
        public async Task<bool> DeleteAllergy(AllergyEntity allergy)
        {
            _context.Allergies.Remove(allergy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}