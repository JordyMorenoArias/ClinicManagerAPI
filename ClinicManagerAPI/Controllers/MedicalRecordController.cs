using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Services.MedicalRecord.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing medical records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MedicalRecordController : Controller
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IUserService _userService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService, IUserService userService)
        {
            this._medicalRecordService = medicalRecordService;
            this._userService = userService;
        }

        /// <summary>
        /// Retrieves a medical record by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the medical record.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the medical record if found; otherwise, a NotFound result.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalRecordById([FromRoute] int id)
        {
            var medicalRecord = await _medicalRecordService.GetMedicalRecordById(id);
            return Ok(medicalRecord);
        }

        /// <summary>
        /// Retrieves a paged list of medical records based on query parameters.
        /// </summary>
        /// <param name="parameters">The filtering and pagination parameters for retrieving medical records.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing a paginated list of medical records.</returns>
        [HttpGet]
        public async Task<IActionResult> GetMedicalRecords([FromQuery] QueryMedicalRecordParameters parameters)
        {
            var medicalRecords = await _medicalRecordService.GetMedicalRecords(parameters);
            return Ok(medicalRecords);
        }

        /// <summary>
        /// Adds a new medical record.
        /// </summary>
        /// <param name="medicalRecordDto">The data for creating the medical record.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the newly created medical record.</returns>
        [HttpPost]
        [Authorize(Policy = "canManageMedicalRecord")]
        public async Task<IActionResult> AddMedicalRecord([FromBody] AddMedicalRecordDto medicalRecordDto)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var medicalRecord = await _medicalRecordService.AddMedicalRecord(userAuthenticated.Id, userAuthenticated.Role, medicalRecordDto);
            return Ok(medicalRecord);
        }

        /// <summary>
        /// Updates an existing medical record.
        /// </summary>
        /// <param name="medicalRecordDto">The data used to update the medical record.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the updated medical record.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "canManageMedicalRecord")]
        public async Task<IActionResult> UpdateMedicalRecord([FromBody] UpdateMedicalRecordDto medicalRecordDto)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var medicalRecord = await _medicalRecordService.UpdateMedicalRecord(userAuthenticated.Id, userAuthenticated.Role, medicalRecordDto);
            return Ok(medicalRecord);
        }

        /// <summary>
        /// Deletes a medical record by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the medical record to delete.</param>
        /// <returns>Returns an <see cref="IActionResult"/> indicating success or failure of the deletion operation.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "canManageMedicalRecord")]
        public async Task<IActionResult> DeleteMedicalRecord([FromRoute] int id)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            await _medicalRecordService.DeleteMedicalRecord(userAuthenticated.Id, userAuthenticated.Role, id);
            return Ok(new { Message = "Medical record deleted successfully." });
        }
    }
}