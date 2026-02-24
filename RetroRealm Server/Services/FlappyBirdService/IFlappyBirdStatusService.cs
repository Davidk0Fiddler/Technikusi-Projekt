using RetroRealm_Server.DTOs.FlappyBirdDTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.FlappyBirdService
{
    public interface IFlappyBirdStatusService
    {
        Task<Result<ReadFlappyBirdStatusDTO>> GetFlappyBirdStatusAsync(string username);
        Task<Result<FlappyBirdStatus>> CreateFlappyBirdStatusAsync(int userId);
        Task<Result<bool>> UpdateFlappyBirdStatusAsync(UpdateFlappyBirdStatusDTO updatedStatus, string username);
    }
}
