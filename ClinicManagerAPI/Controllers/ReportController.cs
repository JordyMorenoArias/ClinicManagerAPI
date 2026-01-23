using ClinicManagerAPI.Models.DTOs.Report;
using ClinicManagerAPI.Services.Report.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerAPI.Controllers
{
    /// <summary>
    /// Controller for generating reports.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="reportService"></param>
        public ReportController(IReportService reportService)
        {
            this._reportService = reportService;
        }

        /// <summary>
        /// Generates a report summary based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="ReportSummaryDto"/> containing the report summary.</returns>
        [HttpGet("summary")]
        [Authorize(Policy = "canManageReports")]
        public async Task<IActionResult> GetReportSummary([FromQuery] QueryReportParameters parameters)
        {
            var reportSummary = await _reportService.GenerateReportAsync(parameters);
            return Ok(reportSummary);
        }
    }
}