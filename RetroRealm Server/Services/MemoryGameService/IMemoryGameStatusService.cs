using RetroRealm_Server.DTOs.MemoryGameDTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.MemoryGameService
{
    public interface IMemoryGameStatusService
    {
        Task<Result<ReadMemoryGameStatusDTO>> GetMemoryGameStatusAsync(string username);
        Task<Result<MemoryGameStatus>> CreateMemoryGameStatusAsync(int userId);
        Task<Result<bool>> UpdateMemoryGameStatusAsync(UpdateMemoryGameStatusDTO updatedStatus, string username);
    }
}
