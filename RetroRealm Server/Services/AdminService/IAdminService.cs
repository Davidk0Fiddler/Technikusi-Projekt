using RetroRealm_Server.DTOs.AdminPanelDTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.AdminService
{
    public interface IAdminService
    {
        Task<Result<List<ReadUserForAdminDTO>>> GetAllUsersForAdminPanel();
        Task<Result<List<ReadLogDTO>>> GetAllLogsForAdminPanel();
    }
}
