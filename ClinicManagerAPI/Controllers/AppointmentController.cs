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
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentById(appointmentId);
            return Ok(appointment);
        }

        /// <summary>
        /// Retrieves a paged list of appointments based on query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery] QueryAppointmentParameters parameters)
        {
            var appointments = await _appointmentService.GetAppointments(parameters);
            return Ok(appointments);
        }

        /// <summary>
        /// Adds a new appointment.
        /// </summary>
        /// <param name="newAppointment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] AddAppointmentDto newAppointment)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var createdAppointment = await _appointmentService.AddAppointment(userAuthenticated.Id, newAppointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = createdAppointment.Id }, createdAppointment);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="updatedAppointment"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentDto updatedAppointment)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var appointment = await _appointmentService.UpdateAppointment(userAuthenticated.Id, updatedAppointment);
            return Ok(appointment);
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment([FromQuery] int appointmentId)
        {
            var userAuthenticated = _userService.GetAuthenticatedUser(HttpContext);
            var result = await _appointmentService.DeleteAppointment(appointmentId);
            return Ok(result);
        }
    }
}