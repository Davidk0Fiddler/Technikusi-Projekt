using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IFlappyBirdStatusService
    {
        Task<Result<ReadFlappyBirdStatusDTO>> GetFlappyBirdStatusAsync(string username);
        Task<Result<FlappyBirdStatus>> CreateFlappyBirdStatusAsync(int userId);
        Task<Result<bool>> UpdateFlappyBirdStatusAsync(UpdateFlappyBirdStatusDTO updatedStatus, string username);
    }
}
