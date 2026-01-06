using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Result<List<ReadRoleDTO>>> GetAllRolesAsync(); // Read (All)
        Task<Result<ReadRoleDTO>> GetRoleAsync(int id); // Read (One)
        Task<Result<Role>> CreateRoleAsync(CreateRoleDTO newRole); // Create
        Task<Result<bool>> UpdateRoleAsync(int id, UpdateRoleDTO updatedRole); // Update
        Task<Result<bool>> DeleteRoleByIdAsync(int id); // Delete
    }
}
