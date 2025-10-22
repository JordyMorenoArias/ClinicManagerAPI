using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Services.Patient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById([FromRoute] int id)
        {
            var patient = await _patientService.GetPatientById(id);
            return Ok(patient);
        }

        /// <summary>
        /// Retrieves a patient by their identification number.
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        [HttpGet("by-identification/{identification}")]
        public async Task<IActionResult> GetPatientByIdentification([FromRoute] string identification)
        {
            var patient = await _patientService.GetPatientByIdentification(identification);
            return Ok(patient);
        }

        /// <summary>
        /// Retrieves a paged list of patients based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet("Patients")]
        public async Task<IActionResult> GetPatientsPagedAsync([FromQuery] QueryPatientParameters parameters)
        {
            var patients = await _patientService.GetPatientsPagedAsync(parameters);
            return Ok(patients);
        }

        /// <summary>
        /// Adds a new patient to the system.
        /// </summary>
        /// <param name="addPatientDto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddPatient([FromBody] AddPatientDto addPatientDto)
        {
            var patient = await _patientService.AddPatient(addPatientDto);
            return Ok(patient);
        }

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="updatePatientDto"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientDto updatePatientDto)
        {
            var patient = await _patientService.UpdatePatient(updatePatientDto);
            return Ok(patient);
        }
    }
}