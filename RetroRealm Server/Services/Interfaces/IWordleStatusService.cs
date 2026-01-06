using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IWordleStatusService
    {
        Task<Result<ReadWordleStatusDTO>> GetWordleStatusAsync(RefreshTokenDto model);
        Task<Result<WordleStatus>> CreateWordleStatusAsync(int userId);
        Task<Result<bool>> UpdateWordleStatusAsync(UpdateWordleStatusDTO updatedStatus, RefreshTokenDto model);
    }
}
