using Microsoft.AspNetCore.Authorization;

namespace RetroRealm_Server.Models
{
    public class RoleOrRoleIdHandler
    : AuthorizationHandler<RoleOrRoleIdRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoleOrRoleIdRequirement requirement)
        {
            var roleClaim = context.User.FindFirst("Role")?.Value;
            var roleIdClaim = context.User.FindFirst("RoleId")?.Value;

            if (
                roleClaim == requirement.RoleName ||
                (int.TryParse(roleIdClaim, out var roleId) && roleId == requirement.RoleId)
            )
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
