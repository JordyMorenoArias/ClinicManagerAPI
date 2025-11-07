using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Services.DoctorProfile.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing doctor profiles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorProfileController : ControllerBase
    {
        private readonly IDoctorProfileService _doctorProfileService;
        private readonly IUserService authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorProfileController"/> class.
        /// </summary>
        /// <param name="doctorProfileService"></param>
        /// <param name="authService"></param>
        public DoctorProfileController(IDoctorProfileService doctorProfileService, IUserService authService)
        {
            this._doctorProfileService = doctorProfileService;
            this.authService = authService;
        }

        /// <summary>
        /// Retrieves a doctor profile by the doctor's unique identifier.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A <see cref="DoctorProfileDto"/> object representing the doctor profile with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorProfileById(int doctorId)
        {
            var doctorProfile = await _doctorProfileService.GetDoctorProfileById(doctorId);
            return Ok(doctorProfile);
        }

        /// <summary>
        /// Retrieves a paged list of doctor profiles based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A <see cref="PagedResult{DoctorProfileDto}"/> containing the paged list of doctor profiles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetDoctorProfiles([FromQuery] QueryDoctorProfileParameters parameters)
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
        public async Task<IActionResult> AddDoctorProfile([FromBody] AddDoctorProfileDto addDoctorProfileDto)
        {
            var userAuthenticated = authService.GetAuthenticatedUser(HttpContext);
            var createdDoctorProfile = await _doctorProfileService.AddDoctorProfile(userAuthenticated.Role, addDoctorProfileDto);
            return Ok(createdDoctorProfile);
        }

        /// <summary>
        /// Updates an existing doctor profile.
        /// </summary>
        /// <param name="updateDoctorProfileDto"></param>
        /// <returns> The updated doctor profile.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctorProfile([FromBody] UpdateDoctorProfileDto updateDoctorProfileDto)
        {
            var userAuthenticated = authService.GetAuthenticatedUser(HttpContext);
            var updatedDoctorProfile = await _doctorProfileService.UpdateDoctorProfile(userAuthenticated.Role, updateDoctorProfileDto);
            return Ok(updatedDoctorProfile);
        }

        /// <summary>
        /// Deletes a doctor profile by doctor ID.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A success message upon deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorProfile([FromQuery] int id)
        {
            var userAuthenticated = authService.GetAuthenticatedUser(HttpContext);
            await _doctorProfileService.DeleteDoctorProfile(userAuthenticated.Role, id);
            return Ok("Doctor profile deleted successfully.");
        }

    }
}