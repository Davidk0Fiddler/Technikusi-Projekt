using RetroRealm_Server.DTOs.BunnyRunDTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.BunnyRunService
{
    public interface IBunnyRunStatusService
    {
        Task<Result<ReadBunnyRunStatusDTO>> GetBunnyRunStatusAsync(string username);
        Task<Result<BunnyRunStatus>> CreateBunnyRunStatusAsync(int userId);
        Task<Result<UpdateBunnyRunStatusDTO>> UpdateBunnyRunStatusAsync(UpdateBunnyRunStatusDTO updatedStatus, string username);
    }
}
