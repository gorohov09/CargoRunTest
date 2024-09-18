using Microsoft.AspNetCore.Authorization;
using IAuthorizationService = Library.Core.Abstractions.IAuthorizationService;

namespace Library.WebAPI.Authentication
{
    /// <summary>
	/// Обработчик требования Привилегий
	/// </summary>
	public class PermissionAuthorizationHandler :
        AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        /// <inheritdoc />
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HttpContext httpContext,
            PermissionAuthorizationRequirement requirement,
            IEnumerable<PermissionAttribute> attributes,
            CancellationToken cancellationToken)
        {
            var privilege = attributes.FirstOrDefault()?.Privilege;
            if (!privilege.HasValue)
            {
                context.Succeed(requirement);
                return;
            }

            var authService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            await authService.CheckPrivilegeAsync(privilege.Value);
            context.Succeed(requirement);
        }
    }
}
