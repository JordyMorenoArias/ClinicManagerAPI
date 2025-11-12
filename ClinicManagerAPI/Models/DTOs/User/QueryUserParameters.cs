using ClinicManagerAPI.Constants;

namespace ClinicManagerAPI.Models.DTOs.User
{
    public class QueryUserParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTime? StartDateFilter { get; set; }

        public DateTime? EndDateFilter { get; set; }

        public bool? IsActive { get; set; }

        public string? Role { get; set; }

        public UserRole? UserRole { get; set; }

        public string? SearchTerm { get; set; }
    }
}