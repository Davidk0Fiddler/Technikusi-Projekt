using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IBunnyRunStatusService
    {
        Task<Result<ReadBunnyRunStatusDTO>> GetBunnyRunStatusAsync(string username);
        Task<Result<BunnyRunStatus>> CreateBunnyRunStatusAsync(int userId);
        Task<Result<UpdateBunnyRunStatusDTO>> UpdateBunnyRunStatusAsync(UpdateBunnyRunStatusDTO updatedStatus, string username);
    }
}
