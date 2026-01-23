using ClinicManagerAPI.Models.DTOs.PatientAllergy;
using ClinicManagerAPI.Services.PatientAllergy.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing patient allergies.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientAllergyController : ControllerBase
    {
        private readonly IPatientAllergyService _patientAllergyService;

        /// <summary>
        /// Constructor for PatientAllergyController.
        /// </summary>
        /// <param name="patientAllergyService"></param>
        public PatientAllergyController(IPatientAllergyService patientAllergyService)
        {
            this._patientAllergyService = patientAllergyService;
        }

        /// <summary>
        /// Get a patient allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the PatientAllergyDto.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientAllergyById(int id)
        {
            var patientAllergy = await _patientAllergyService.GetPatientAllergyById(id);
            return Ok(patientAllergy);
        }

        /// <summary>
        /// Get a paginated list of patient allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the PagedResult of PatientAllergyDto.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPatientAllergies([FromQuery] PatientAllergyQueryParameters parameters)
        {
            var pagedResult = await _patientAllergyService.GetPatientAllergies(parameters);
            return Ok(pagedResult);
        }

        /// <summary>
        /// Add a new patient allergy.
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the created PatientAllergyDto.</returns>
        [HttpPost]
        [Authorize(Policy = "canManagePatientAllergies")]
        public async Task<IActionResult> AddPatientAllergy([FromBody] AddPatientAllergyDto createDto)
        {
            var createdPatientAllergy = await _patientAllergyService.AddPatientAllergy(createDto);
            return CreatedAtAction(nameof(GetPatientAllergyById), new { id = createdPatientAllergy.Id }, createdPatientAllergy);
        }

        /// <summary>
        /// Update an existing patient allergy.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatePatientAllergyDto"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the updated PatientAllergyDto.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "canManagePatientAllergies")]
        public async Task<IActionResult> UpdatePatientAllergy(int id, [FromBody] UpdatePatientAllergyDto updatePatientAllergyDto)
        {
            var updatedPatientAllergy = await _patientAllergyService.UpdatePatientAllergy(id, updatePatientAllergyDto);
            return Ok(updatedPatientAllergy);
        }

        /// <summary>
        /// Delete a patient allergy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> An <see cref="IActionResult"/> indicating the result of the deletion.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "canManagePatientAllergies")]
        public async Task<IActionResult> DeletePatientAllergy(int id)
        {
            var result = await _patientAllergyService.DeletePatientAllergy(id);
            return Ok(result);
        }
    }
}