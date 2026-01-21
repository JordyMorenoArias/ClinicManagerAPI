 using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.Allergy;
using ClinicManagerAPI.Services.Allergy.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing allergies.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AllergyController : Controller
    {
        private readonly IAllergyService _allergyService;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for AllergyController.
        /// </summary>
        /// <param name="allergyService"></param>
        /// <param name="userService"></param>
        public AllergyController(IAllergyService allergyService, IUserService userService)
        {
            this._allergyService = allergyService;
            this._userService = userService;
        }

        /// <summary>
        /// Get an allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllergyBiId([FromRoute] int id)
        {
            var allergy = await _allergyService.GetAllergyById(id);
            return Ok(allergy);
        }

        /// <summary>
        /// Get a paginated list of allergies based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllergies([FromQuery] QueryAllergyParameters parameters)
        {
            var allergies = await _allergyService.GetAllergies(parameters);
            return Ok(allergies);
        }

        /// <summary>
        /// Create a new allergy.
        /// </summary>
        /// <param name="createAllergyDto"></param>
        /// <returns> a task that represents the asynchronous operation. The task result contains the IActionResult.</returns>
        [HttpPost]
        [Authorize(Policy = "canManageAllergies")]
        public async Task<IActionResult> CreateAllergy([FromBody] CreateAllergyDto createAllergyDto)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var createdAllergy = await _allergyService.CreateAllergy(userAuthenticated.Role, createAllergyDto);
            return CreatedAtAction(nameof(GetAllergyBiId), new { id = createdAllergy.Id }, createdAllergy);
        }

        /// <summary>
        /// Updates an existing allergy.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAllergyDto"></param>
        /// <returns> The updated allergy.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "canManageAllergies")]
        public async Task<IActionResult> UpdateAllergy([FromRoute] int id, [FromBody] UpdateAllergyDto updateAllergyDto)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var updatedAllergy = await _allergyService.UpdateAllergy(userAuthenticated.Role, id, updateAllergyDto);
            return Ok(updatedAllergy);
        }

        /// <summary>
        /// Deletes an allergy by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A success message upon deletion.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "canManageAllergies")]
        public async Task<IActionResult> DeleteAllergy([FromRoute] int id)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var result = await _allergyService.DeleteAllergy(userAuthenticated.Role, id);
            return Ok(result);
        }
    }
}