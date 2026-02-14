using ClinicManagerAPI.Authorization.Requirements;
using ClinicManagerAPI.Constants;
using Microsoft.AspNetCore.Authorization;

namespace ClinicManagerAPI.Authorization.Handlers
{
    /// <summary>
    /// Authorization handler for updating user information.
    /// </summary>
    public class UpdateUserHandler : AuthorizationHandler<UpdateUserRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Handles the authorization requirement for updating user information.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UpdateUserRequirement requirement)
        {
            var http = _httpContextAccessor.HttpContext;

            var userIdAuthenticated = context.User.FindFirst("id")?.Value;
            var targetId = http!.Request.RouteValues["id"]?.ToString();

            var role = context.User.FindFirst("role")?.Value;

            // Only admins can edit other users
            if (role == Roles.Admin)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Users can edit their own profile
            if (userIdAuthenticated == targetId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}