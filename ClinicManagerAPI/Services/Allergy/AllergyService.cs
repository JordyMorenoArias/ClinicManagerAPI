using AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Allergy;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.Allergy.Interfaces;

namespace ClinicManagerAPI.Services.Allergy
{
    /// <summary>
    /// Service for managing allergies.
    /// </summary>
    public class AllergyService : IAllergyService
    {
        private readonly IAllergyRepository _allergyRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for AllergyService.
        /// </summary>
        /// <param name="allergyRepository"></param>
        /// <param name="mapper"></param>
        public AllergyService(IAllergyRepository allergyRepository, IMapper mapper)
        {
            this._allergyRepository = allergyRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get an allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the AllergyDto.</returns>
        public async Task<AllergyDto> GetAllergyById(int id)
        {
            var allergy = await _allergyRepository.GetAllergyById(id);
            return _mapper.Map<AllergyDto>(allergy);
        }

        /// <summary>
        /// Get a paginated list of allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the PagedResult of AllergyDto.</returns>
        public async Task<PagedResult<AllergyDto>> GetAllergies(QueryAllergyParameters parameters)
        {
            var pagedAllergies = await _allergyRepository.GetAllergies(parameters);
            var mappedAllergies = _mapper.Map<IEnumerable<AllergyDto>>(pagedAllergies.Items);
            return new PagedResult<AllergyDto>
            {
                Items = mappedAllergies,
                TotalItems = pagedAllergies.TotalItems,
                Page = pagedAllergies.Page,
                PageSize = pagedAllergies.PageSize
            };
        }

        /// <summary>
        /// Create a new allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="createAllergyDto"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the created AllergyDto.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<AllergyDto> CreateAllergy(UserRole requestRole, CreateAllergyDto createAllergyDto)
        {
            if (requestRole != UserRole.doctor && requestRole != UserRole.admin)
                throw new UnauthorizedAccessException("Only doctors and admins can create allergies.");

            var allergyEntity = _mapper.Map<AllergyEntity>(createAllergyDto);
            var createdAllergy = await _allergyRepository.CreateAllergy(allergyEntity);
            return _mapper.Map<AllergyDto>(createdAllergy);
        }

        /// <summary>
        /// Update an existing allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <param name="updateAllergyDto"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the updated AllergyDto.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<AllergyDto> UpdateAllergy(UserRole requestRole, int id, UpdateAllergyDto updateAllergyDto)
        {
            if (requestRole != UserRole.doctor && requestRole != UserRole.admin)
                throw new UnauthorizedAccessException("Only doctors and admins can update allergies.");

            var existingAllergy = await _allergyRepository.GetAllergyById(id);

            if (existingAllergy == null)
                throw new KeyNotFoundException($"Allergy with ID {id} not found.");

            _mapper.Map(updateAllergyDto, existingAllergy);
            var updatedAllergy = await _allergyRepository.UpdateAllergy(existingAllergy);
            return _mapper.Map<AllergyDto>(updatedAllergy);
        }

        /// <summary>
        /// Delete an allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <returns>An <see cref="OperationResult"/> indicating the result of the deletion.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<OperationResult> DeleteAllergy(UserRole requestRole, int id)
        {
            if (requestRole != UserRole.admin)
                throw new UnauthorizedAccessException("Only admins can delete allergies.");

            var existingAllergy = await _allergyRepository.GetAllergyById(id);

            if (existingAllergy == null)
                throw new KeyNotFoundException($"Allergy with ID {id} not found.");

            var success = await _allergyRepository.DeleteAllergy(existingAllergy);

            return new OperationResult
            {
                Success = success,
                Message = "Allergy deleted successfully."
            };
        }
    }
}