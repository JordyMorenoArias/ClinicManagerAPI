using AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.PatientAllergy;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.PatientAllergy.Interfaces;

namespace ClinicManagerAPI.Services.PatientAllergy
{
    /// <summary>
    /// Service for managing patient allergies.
    /// </summary>
    public class PatientAllergyService : IPatientAllergyService
    {
        private readonly IPatientAllergyRepository _patientAllergyRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for PatientAllergyService.
        /// </summary>
        /// <param name="patientAllergyRepository"></param>
        /// <param name="mapper"></param>
        public PatientAllergyService(IPatientAllergyRepository patientAllergyRepository, IMapper mapper)
        {
            this._patientAllergyRepository = patientAllergyRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get a patient allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the PatientAllergyDto.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<PatientAllergyDto> GetPatientAllergyById(int id)
        {
            var patientAllergyEntity = await _patientAllergyRepository.GetPatientAllergyById(id);

            if (patientAllergyEntity == null)
                throw new KeyNotFoundException($"PatientAllergy with ID {id} not found.");

            return _mapper.Map<PatientAllergyDto>(patientAllergyEntity);
        }

        /// <summary>
        /// Get a paginated list of patient allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the PagedResult of PatientAllergyDto.</returns>
        public async Task<PagedResult<PatientAllergyDto>> GetPatientAllergies(QueryPatientAllergyParameters parameters)
        {
            var pagedPatientAllergies = await _patientAllergyRepository.GetPatientAllergies(parameters);

            var mappedItems = pagedPatientAllergies.Items
                .Select(pa => _mapper.Map<PatientAllergyDto>(pa))
                .ToList();

            return new PagedResult<PatientAllergyDto>
            {
                Items = mappedItems,
                TotalItems = pagedPatientAllergies.TotalItems,
                Page = pagedPatientAllergies.Page,
                PageSize = pagedPatientAllergies.PageSize
            };
        }

        /// <summary>
        /// Add a new patient allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="addDto"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the created PatientAllergyDto.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<PatientAllergyDto> AddPatientAllergy(UserRole requestRole, AddPatientAllergyDto addDto)
        {
            if (requestRole != UserRole.admin && requestRole != UserRole.doctor)
                throw new UnauthorizedAccessException("Only Admins and Doctors can add patient allergies.");

            var patientAllergyEntity = _mapper.Map<PatientAllergyEntity>(addDto);
            var createdEntity = await _patientAllergyRepository.AddPatientAllergy(patientAllergyEntity);
            return _mapper.Map<PatientAllergyDto>(createdEntity);
        }

        /// <summary>
        /// Update an existing patient allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the updated PatientAllergyDto.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<PatientAllergyDto> UpdatePatientAllergy(UserRole requestRole, int id, UpdatePatientAllergyDto updateDto)
        {
            if (requestRole != UserRole.admin && requestRole != UserRole.doctor)
                throw new UnauthorizedAccessException("Only Admins and Doctors can update patient allergies.");

            var existingEntity = await _patientAllergyRepository.GetPatientAllergyById(id);

            if (existingEntity == null)
                throw new KeyNotFoundException($"PatientAllergy with ID {id} not found.");

            _mapper.Map(updateDto, existingEntity);
            var updatedEntity = await _patientAllergyRepository.UpdatePatientAllergy(existingEntity);
            return _mapper.Map<PatientAllergyDto>(updatedEntity);
        }

        /// <summary>
        /// Delete a patient allergy.
        /// </summary>
        /// <param name="requestRole"></param>
        /// <param name="id"></param>
        /// <returns> A task that represents the asynchronous operation.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeletePatientAllergy(UserRole requestRole, int id)
        {
            if (requestRole != UserRole.admin && requestRole != UserRole.doctor)
                throw new UnauthorizedAccessException("Only Admins and Doctors can delete patient allergies.");

            var existingEntity = await _patientAllergyRepository.GetPatientAllergyById(id);

            if (existingEntity == null)
                throw new KeyNotFoundException($"PatientAllergy with ID {id} not found.");

            await _patientAllergyRepository.DeletePatientAllergy(existingEntity);
        }
    }
}