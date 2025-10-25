using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Filters;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Services.Appointment;
using ClinicManagerAPI.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for managing appointments.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AuthorizeRole(UserRole.admin, UserRole.doctor, UserRole.assistant)]
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _appointmentService;
        private readonly UserService _userService;

        public AppointmentController(AppointmentService appointmentService, UserService userService)
        {
            this._appointmentService = appointmentService;
            this._userService = userService;
        }

        /// <summary>
        /// Retrieves an appointment by its unique identifier.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to retrieve.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the appointment details 
        /// if found, or a 404 Not Found response if the appointment does not exist.
        /// </returns>
        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentById(appointmentId);
            return Ok(appointment);
        }

        /// <summary>
        /// Retrieves a paginated list of appointments based on query parameters.
        /// </summary>
        /// <param name="parameters">Filtering, sorting, and pagination options for the appointments query.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing a paged list of appointments 
        /// that match the specified query parameters.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery] QueryAppointmentParameters parameters)
        {
            var appointments = await _appointmentService.GetAppointments(parameters);
            return Ok(appointments);
        }

        /// <summary>
        /// Creates a new appointment record.
        /// </summary>
        /// <param name="newAppointment">The data for the new appointment to be created.</param>
        /// <returns>
        /// Returns a <see cref="CreatedAtActionResult"/> containing the newly created appointment 
        /// and a 201 Created HTTP status code.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] AddAppointmentDto newAppointment)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var createdAppointment = await _appointmentService.AddAppointment(userAuthenticated.Id, newAppointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = createdAppointment.Id }, createdAppointment);
        }

        /// <summary>
        /// Updates the details of an existing appointment.
        /// </summary>
        /// <param name="updatedAppointment">The updated appointment information.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the updated appointment data 
        /// if the operation succeeds, or a 404 Not Found response if the appointment does not exist.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentDto updatedAppointment)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var appointment = await _appointmentService.UpdateAppointment(userAuthenticated.Id, updatedAppointment);
            return Ok(appointment);
        }

        /// <summary>
        /// Deletes an appointment by its unique identifier.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to delete.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the delete operation, 
        /// typically a 200 OK response with a confirmation message.
        /// </returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment([FromQuery] int appointmentId)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var result = await _appointmentService.DeleteAppointment(appointmentId);
            return Ok(result);
        }
    }
}