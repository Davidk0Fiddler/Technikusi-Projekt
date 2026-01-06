using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IMemoryGameStatusService
    {
        Task<Result<ReadMemoryGameStatusDTO>> GetMemoryGameStatusAsync(RefreshTokenDto model);
        Task<Result<MemoryGameStatus>> CreateMemoryGameStatusAsync(int userId);
        Task<Result<bool>> UpdateMemoryGameStatusAsync(UpdateMemoryGameStatusDTO updatedStatus, RefreshTokenDto model);
    }
}
