using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Services.Patient.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing patients.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing a <see cref="PatientDto"/> 
        /// with the patient's information if found, or a <c>404 Not Found</c> response if the patient does not exist.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById([FromRoute] int id)
        {
            var patient = await _patientService.GetPatientById(id);
            return Ok(patient);
        }

        /// <summary>
        /// Retrieves a patient by their identification number.
        /// </summary>
        /// <param name="identification">The identification number of the patient (e.g., national ID).</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing a <see cref="PatientDto"/> 
        /// with the patient's details if found, or a <c>404 Not Found</c> response if no match is found.
        /// </returns>
        [HttpGet("by-identification/{identification}")]
        public async Task<IActionResult> GetPatientByIdentification([FromRoute] string identification)
        {
            var patient = await _patientService.GetPatientByIdentification(identification);
            return Ok(patient);
        }

        /// <summary>
        /// Retrieves a paged list of patients based on query parameters.
        /// </summary>
        /// <param name="parameters">The query parameters for pagination and filtering.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing a paged list of patients 
        /// as a <see cref="PagedResult{PatientDto}"/> object. 
        /// Returns an empty list if no patients match the specified filters.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetPatientsPagedAsync([FromQuery] QueryPatientParameters parameters)
        {
            var patients = await _patientService.GetPatients(parameters);
            return Ok(patients);
        }

        /// <summary>
        /// Adds a new patient to the system.
        /// </summary>
        /// <param name="addPatientDto">The data required to create a new patient record.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the newly created <see cref="PatientDto"/> 
        /// if the operation succeeds, or a <c>400 Bad Request</c> response if validation fails or a duplicate exists.
        /// </returns>
        [HttpPost]
        [Authorize(Policy = "canManagePatients")]
        public async Task<IActionResult> AddPatient([FromBody] AddPatientDto addPatientDto)
        {
            var patient = await _patientService.AddPatient(addPatientDto);
            return Ok(patient);
        }

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="updatePatientDto">The data to update the patient's record.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the updated <see cref="PatientDto"/> 
        /// if the update is successful, or a <c>404 Not Found</c> response if the patient does not exist.
        /// </returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "canManagePatients")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientDto updatePatientDto)
        {
            var patient = await _patientService.UpdatePatient(updatePatientDto);
            return Ok(patient);
        }
    }
}