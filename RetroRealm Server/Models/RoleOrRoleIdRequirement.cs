using Microsoft.AspNetCore.Authorization;

namespace RetroRealm_Server.Models
{
    public class RoleOrRoleIdRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; }
        public int RoleId { get; }

        public RoleOrRoleIdRequirement(string roleName, int roleId)
        {
            RoleName = roleName;
            RoleId = roleId;
        }
    }
}
