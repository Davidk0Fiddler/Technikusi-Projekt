using RetroRealm_Server.DTOs.WorldeStatusDTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.WorldeStatusService
{
    public interface IWordleStatusService
    {
        Task<Result<ReadWordleStatusDTO>> GetWordleStatusAsync(string username);
        Task<Result<WordleStatus>> CreateWordleStatusAsync(int userId);
        Task<Result<bool>> UpdateWordleStatusAsync(UpdateWordleStatusDTO updatedStatus, string username);
    }
}
