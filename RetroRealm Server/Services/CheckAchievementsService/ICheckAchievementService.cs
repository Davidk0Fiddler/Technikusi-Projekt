using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.CheckAchievementsService
{
    public interface ICheckAchievementService
    {
        Task<Result<bool>> CheckBunnyRunAchievementsAsync(int userId, int MaxDistance);
        Task<Result<bool>> CheckFlappyBirdAchievementsAsync(int userId, int MaxPassesPipes);
        Task<Result<bool>> CheckMemoryCardAchievementsAsync(int userId, int MinFlipping);
        Task<Result<bool>> CheckWordleAchievementsAsync(int userId, int CompletedWords);
    }
}
