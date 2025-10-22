using AutoMapper;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.Entities;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.Patient.Interfaces;

namespace ClinicManagerAPI.Services.Patient
{
    /// <summary>
    /// Provides operations related to patient management, including retrieval,
    /// </summary>
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            this._patientRepository = patientRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrieves a patient by their unique identifier.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>A <see cref="PatientDto"/> representing the patient with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<PatientDto> GetPatientById(int patientId)
        {
            var patient = await _patientRepository.GetPatientById(patientId);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID {patientId} not found.");

            return _mapper.Map<PatientDto>(patient);
        }

        /// <summary>
        /// Retrieves a patient by their identification number.
        /// </summary>
        /// <param name="identification"></param>
        /// <returns>A <see cref="PatientDto"/> representing the patient with the specified identification number.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<PatientDto> GetPatientByIdentification(string identification)
        {
            var patient = await _patientRepository.GetPatientByIdentification(identification);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with identification {identification} not found.");

            return _mapper.Map<PatientDto>(patient);
        }

        /// <summary>
        /// Retrieves a paged list of patients based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="PagedResult{PatientDto}"/> containing the paged list of patients.</returns>
        public async Task<PagedResult<PatientDto>> GetPatientsPagedAsync(QueryPatientParameters parameters)
        {
            var pagedPatients = await _patientRepository.GetPatientsPagedAsync(parameters);

            var mappedPatients = pagedPatients.Items
                .Select(patient => _mapper.Map<PatientDto>(patient))
                .ToList();

            return new PagedResult<PatientDto>
            {
                Items = mappedPatients,
                TotalItems = pagedPatients.TotalItems,
                Page = pagedPatients.Page,
                PageSize = pagedPatients.PageSize
            };
        }

        /// <summary>
        /// Adds a new patient to the system.
        /// </summary>
        /// <param name="addPatientDto"></param>
        /// <returns>A <see cref="PatientDto"/> representing the newly added patient.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<PatientDto> AddPatient(AddPatientDto addPatientDto)
        {
            var patientExists = await _patientRepository.GetPatientByIdentification(addPatientDto.Identification);

            if (patientExists != null)
                throw new InvalidOperationException($"A patient with identification {addPatientDto.Identification} already exists.");

            var patientEntity = _mapper.Map<PatientEntity>(addPatientDto);
            var createdPatient = await _patientRepository.AddPatient(patientEntity);
            return _mapper.Map<PatientDto>(createdPatient);
        }

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="updatePatientDto"></param>
        /// <returns>A <see cref="PatientDto"/> representing the updated patient.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<PatientDto> UpdatePatient(UpdatePatientDto updatePatientDto)
        {
            var existingPatient = await _patientRepository.GetPatientById(updatePatientDto.Id);

            if (existingPatient == null)
                throw new KeyNotFoundException($"Patient with ID {updatePatientDto.Id} not found.");

            _mapper.Map(updatePatientDto, existingPatient);
            var updatedPatient = await _patientRepository.UpdatePatient(existingPatient);
            return _mapper.Map<PatientDto>(updatedPatient);
        }
    }
}