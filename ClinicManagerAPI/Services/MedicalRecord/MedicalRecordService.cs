using AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.MedicalRecord.Interfaces;

namespace ClinicManagerAPI.Services.MedicalRecord
{
    /// <summary>
    /// Service for managing medical record operations.
    /// </summary>
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            this._medicalRecordRepository = medicalRecordRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrieves a medical record by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="MedicalRecordDto"/> object representing the medical record with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<MedicalRecordDto> GetMedicalRecordById(int id)
        {
            var medicalRecordEntity = await _medicalRecordRepository.GetMedicalRecordById(id);

            if (medicalRecordEntity == null)
                throw new KeyNotFoundException($"Medical record with ID {id} not found.");

            return _mapper.Map<MedicalRecordDto>(medicalRecordEntity);
        }

        /// <summary>
        /// Retrieves a paged list of medical records based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{MedicalRecordDto}"/> containing the paged list of medical records.</returns>
        public async Task<PagedResult<MedicalRecordDto>> GetMedicalRecords(QueryMedicalRecordParameters parameters)
        {
            var medicalRecordsEntity = await _medicalRecordRepository.GetMedicalRecords(parameters);

            var medicalRecordsDto = _mapper.Map<List<MedicalRecordDto>>(medicalRecordsEntity.Items);

            return new PagedResult<MedicalRecordDto>
            {
                Items = medicalRecordsDto,
                TotalItems = medicalRecordsEntity.TotalItems,
                Page = medicalRecordsEntity.Page,
                PageSize = medicalRecordsEntity.PageSize
            };
        }

        /// <summary>
        /// Adds a new medical record.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="requestRole"></param>
        /// <param name="medicalRecordDto"></param>
        /// <returns>The added <see cref="MedicalRecordDto"/>.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<MedicalRecordDto> AddMedicalRecord(int requestId, UserRole requestRole, AddMedicalRecordDto medicalRecordDto)
        {
            if (requestRole == UserRole.assistant)
                throw new UnauthorizedAccessException("Assistants are not allowed to add medical records.");

            if (requestRole == UserRole.doctor && medicalRecordDto.DoctorId != requestId)
                throw new UnauthorizedAccessException("Doctors can only add medical records for themselves.");

            var medicalRecordEntity = _mapper.Map<Models.Entities.MedicalRecordEntity>(medicalRecordDto);
            await _medicalRecordRepository.AddMedicalRecord(medicalRecordEntity);
            return _mapper.Map<MedicalRecordDto>(medicalRecordEntity);
        }

        /// <summary>
        /// Updates an existing medical record.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="requestRole"></param>
        /// <param name="medicalRecordDto"></param>
        /// <returns>The updated <see cref="MedicalRecordDto"/>.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<MedicalRecordDto> UpdateMedicalRecord(int requestId, UserRole requestRole, UpdateMedicalRecordDto medicalRecordDto)
        {
            if (requestRole == UserRole.assistant)
                throw new UnauthorizedAccessException("Assistants are not allowed to update medical records.");

            var existingMedicalRecord = await _medicalRecordRepository.GetMedicalRecordById(medicalRecordDto.Id);

            if (existingMedicalRecord == null)
                throw new KeyNotFoundException($"Medical record with ID {medicalRecordDto.Id} not found.");

            if (requestRole == UserRole.doctor && existingMedicalRecord.DoctorId != requestId)
                throw new UnauthorizedAccessException("Doctors can only update their own medical records.");

            _mapper.Map(medicalRecordDto, existingMedicalRecord);
            await _medicalRecordRepository.UpdateMedicalRecord(existingMedicalRecord);
            return _mapper.Map<MedicalRecordDto>(existingMedicalRecord);
        }

        /// <summary>
        /// Deletes a medical record.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="requestRole"></param>
        /// <param name="medicalRecordId"></param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteMedicalRecord(int requestId, UserRole requestRole, int medicalRecordId)
        {
            if (requestRole == UserRole.assistant)
                throw new UnauthorizedAccessException("Assistants are not allowed to delete medical records.");

            var existingMedicalRecord = await _medicalRecordRepository.GetMedicalRecordById(medicalRecordId);

            if (existingMedicalRecord == null)
                throw new KeyNotFoundException($"Medical record with ID {medicalRecordId} not found.");

            if (requestRole == UserRole.doctor && existingMedicalRecord.DoctorId != requestId)
                throw new UnauthorizedAccessException("Doctors can only delete their own medical records.");

            await _medicalRecordRepository.DeleteMedicalRecord(existingMedicalRecord);
        }
    }
}