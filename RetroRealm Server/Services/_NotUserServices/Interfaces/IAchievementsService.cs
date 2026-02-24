using RetroRealm_Server.DTOs;
using RetroRealm_Server.DTOs._NotUserDTOS;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services._NotUserServices.Interfaces
{
    public interface IAchievementsService
    {
        Task<Result<List<ReadAchievementDTO>>> GetAllAchievementsAsync();
        Task<Result<ReadAchievementDTO>> GetAchievementAsync(int id);
        Task<Result<Achievement>> CreateAchievementAsync(CreateAchievementDTO newAchivement);
        Task<Result<UpdateAchievementDTO>> UpdateAchievementAsync(int id, UpdateAchievementDTO updatedAchivement);
        Task<Result<int>> DeleteAchievementByIdAsync(int id);
    }
}
