using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Services.DoctorProfile.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing doctor profiles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DoctorProfileController : ControllerBase
    {
        private readonly IDoctorProfileService _doctorProfileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorProfileController"/> class.
        /// </summary>
        /// <param name="doctorProfileService"></param>
        /// <param name="authService"></param>
        public DoctorProfileController(IDoctorProfileService doctorProfileService)
        {
            this._doctorProfileService = doctorProfileService;
        }

        /// <summary>
        /// Retrieves a doctor profile by the doctor's unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A <see cref="DoctorProfileDto"/> object representing the doctor profile with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorProfileById([FromRoute] int id)
        {
            var doctorProfile = await _doctorProfileService.GetDoctorProfileById(id);
            return Ok(doctorProfile);
        }
        
        /// <summary>
        /// Retrieves a paged list of doctor profiles based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A <see cref="PagedResult{DoctorProfileDto}"/> containing the paged list of doctor profiles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetDoctorProfiles([FromQuery] DoctorProfileQueryParameters parameters)
        {
            var pagedDoctorProfiles = await _doctorProfileService.GetDoctorProfiles(parameters);
            return Ok(pagedDoctorProfiles);
        }

        /// <summary>
        /// Adds a new doctor profile.
        /// </summary>
        /// <param name="addDoctorProfileDto"></param>
        /// <returns> The created doctor profile.</returns>
        [HttpPost]
        [Authorize(Policy = "canManageDoctorProfiles")]
        public async Task<IActionResult> AddDoctorProfile([FromBody] AddDoctorProfileDto addDoctorProfileDto)
        {
            var createdDoctorProfile = await _doctorProfileService.AddDoctorProfile(addDoctorProfileDto);
            return Ok(createdDoctorProfile);
        }

        /// <summary>
        /// Updates an existing doctor profile.
        /// </summary>
        /// <param name="updateDoctorProfileDto"></param>
        /// <returns> The updated doctor profile.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "canManageDoctorProfiles")]
        public async Task<IActionResult> UpdateDoctorProfile([FromRoute] int id, [FromBody] UpdateDoctorProfileDto updateDoctorProfileDto)
        {
            var updatedDoctorProfile = await _doctorProfileService.UpdateDoctorProfile(id, updateDoctorProfileDto);
            return Ok(updatedDoctorProfile);
        }

        /// <summary>
        /// Deletes a doctor profile by doctor ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A success message upon deletion.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "canManageDoctorProfiles")]
        public async Task<IActionResult> DeleteDoctorProfile([FromRoute] int id)
        {
            await _doctorProfileService.DeleteDoctorProfile(id);
            return Ok("Doctor profile deleted successfully.");
        }
    }
}