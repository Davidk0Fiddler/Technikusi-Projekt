using RetroRealm_Server.DTOs.GetAchievementsByUser;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.GetAchievementsByUserService
{
    public interface IGetAchievementsByUserService
    {
        Task<Result<List<GetAchievementsByUserDTO>>> GetAchievementsByUser(string userName);
    }
}
