using ClinicManagerAPI.Models.DTOs.Report;

namespace ClinicManagerAPI.Services.Report.Interfaces
{
    /// <summary>
    /// Provides services for generating reports.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Generates a comprehensive report based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>A <see cref="ReportSummaryDto"/> containing the report summary.</returns>
        Task<ReportSummaryDto> GenerateReportAsync(QueryReportParameters parameters);
    }
}