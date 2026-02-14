using AutoMapper;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.MedicalRecord.Interfaces;
using ClinicManagerAPI.Services.Patient.Interfaces;

namespace ClinicManagerAPI.Services.MedicalRecord
{
    /// <summary>
    /// Service for managing medical record operations.
    /// </summary>
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IPatientService patientService, IMapper mapper)
        {
            this._medicalRecordRepository = medicalRecordRepository;
            this._patientService = patientService;
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
        public async Task<PagedResult<MedicalRecordDto>> GetMedicalRecords(MedicalRecordQueryParameters parameters)
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
        /// <param name="medicalRecordDto"></param>
        /// <returns>The added <see cref="MedicalRecordDto"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>"
        public async Task<MedicalRecordDto> AddMedicalRecord(int requestId, AddMedicalRecordDto medicalRecordDto)
        {
            var patient = await _patientService.GetPatientById(medicalRecordDto.PatientId);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {medicalRecordDto.PatientId} not found.");

            var medicalRecordEntity = _mapper.Map<MedicalRecordEntity>(medicalRecordDto);
            medicalRecordEntity.DoctorId = requestId;
            await _medicalRecordRepository.AddMedicalRecord(medicalRecordEntity);
            return _mapper.Map<MedicalRecordDto>(medicalRecordEntity);
        }

        /// <summary>
        /// Updates an existing medical record.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="id"></param>
        /// <param name="medicalRecordDto"></param>
        /// <returns>The updated <see cref="MedicalRecordDto"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>"
        public async Task<MedicalRecordDto> UpdateMedicalRecord(int requestId, int id, UpdateMedicalRecordDto medicalRecordDto)
        {
            var existingRecord = await _medicalRecordRepository.GetMedicalRecordById(id);

            if (existingRecord == null)
                throw new KeyNotFoundException($"Medical record with ID {id} not found.");

            if (existingRecord.DoctorId != requestId)
                throw new UnauthorizedAccessException("You are not authorized to update this medical record.");

            _mapper.Map(medicalRecordDto, existingRecord);
            await _medicalRecordRepository.UpdateMedicalRecord(existingRecord);
            return _mapper.Map<MedicalRecordDto>(existingRecord);
        }
    }
}