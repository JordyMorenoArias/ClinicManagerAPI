using ClinicManagerAPI.Models.DTOs.PatientAllergy;
using ClinicManagerAPI.Services.PatientAllergy.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing patient allergies.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PatientAllergyController : ControllerBase
    {
        private readonly IPatientAllergyService _patientAllergyService;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for PatientAllergyController.
        /// </summary>
        /// <param name="patientAllergyService"></param>
        /// <param name="userService"></param>
        public PatientAllergyController(IPatientAllergyService patientAllergyService, IUserService userService)
        {
            this._patientAllergyService = patientAllergyService;
            this._userService = userService;
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
        public async Task<IActionResult> GetPatientAllergies([FromQuery] QueryPatientAllergyParameters parameters)
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
        public async Task<IActionResult> AddPatientAllergy([FromBody] AddPatientAllergyDto createDto)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var createdPatientAllergy = await _patientAllergyService.AddPatientAllergy(userAuthenticated.Role, createDto);
            return CreatedAtAction(nameof(GetPatientAllergyById), new { id = createdPatientAllergy.Id }, createdPatientAllergy);
        }

        /// <summary>
        /// Update an existing patient allergy.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatePatientAllergyDto"></param>
        /// <returns> A task that represents the asynchronous operation. The task result contains the updated PatientAllergyDto.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatientAllergy(int id, [FromBody] UpdatePatientAllergyDto updatePatientAllergyDto)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var updatedPatientAllergy = await _patientAllergyService.UpdatePatientAllergy(userAuthenticated.Role, id, updatePatientAllergyDto);
            return Ok(updatedPatientAllergy);
        }

        /// <summary>
        /// Delete a patient allergy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> An <see cref="IActionResult"/> indicating the result of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientAllergy(int id)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var result = await _patientAllergyService.DeletePatientAllergy(userAuthenticated.Role, id);
            return Ok(result);
        }
    }
}