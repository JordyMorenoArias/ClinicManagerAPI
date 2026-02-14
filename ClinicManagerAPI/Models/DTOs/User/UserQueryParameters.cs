using ClinicManagerAPI.Constants;

namespace ClinicManagerAPI.Models.DTOs.User
{
    public class UserQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTimeOffset? StartDateFilter { get; set; }

        public DateTimeOffset? EndDateFilter { get; set; }

        public bool? IsActive { get; set; }

        public string? Role { get; set; }

        public UserRole? UserRole { get; set; }

        public string? SearchTerm { get; set; }
    }
}