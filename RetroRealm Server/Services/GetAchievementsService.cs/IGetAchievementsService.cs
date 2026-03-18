using RetroRealm_Server.DTOs.GetAchievementsDTO;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.GetAchievementsService.cs
{
    public interface IGetAchievementsService
    {
        Task<Result<List<GetAchievementDTO>>> GetAchievementsAsync();
    }
}
