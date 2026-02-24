using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.CheckAchievementsService
{
    public class CheckAchievementService : ICheckAchievementService
    {
        private readonly RetroRealmDatabaseContext _context;
        public CheckAchievementService(RetroRealmDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> CheckBunnyRunAchievementsAsync(int userId, int MaxDistance)
        {
            var user = await _context.Users.FindAsync(userId);

            var achievements = await _context.Achievements.Where(a => a.GameId == 1).ToListAsync();

            foreach (var achievement in achievements)
            {
                if (!user.CompletedChallangesId.Contains(achievement.Id) && achievement.Requirement <= MaxDistance)
                {
                    user.CompletedChallangesId.Add(achievement.Id);
                    user.Coins = user.Coins + achievement.PrizeCoin;
                }
            }

            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> CheckFlappyBirdAchievementsAsync(int userId, int MaxPassesPipes)
        {
            var user = await _context.Users.FindAsync(userId);
            var achievements = await _context.Achievements.Where(a => a.GameId == 2).ToListAsync();
            foreach (var achievement in achievements)
            {
                if (!user.CompletedChallangesId.Contains(achievement.Id) && achievement.Requirement <= MaxPassesPipes)
                {
                    user.CompletedChallangesId.Add(achievement.Id);
                    user.Coins = user.Coins + achievement.PrizeCoin;
                }
            }
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> CheckMemoryCardAchievementsAsync(int userId, int MinFlipping)
        {
            var user = await _context.Users.FindAsync(userId);
            var achievements = await _context.Achievements.Where(a => a.GameId == 3).ToListAsync();
            foreach (var achievement in achievements)
            {
                if (!user.CompletedChallangesId.Contains(achievement.Id) && achievement.Requirement <= MinFlipping)
                {
                    user.CompletedChallangesId.Add(achievement.Id);
                    user.Coins = user.Coins + achievement.PrizeCoin;
                }
            }
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> CheckWordleAchievementsAsync(int userId, int CompletedWords)
        {
            var user = await _context.Users.FindAsync(userId);
            var achievements = await _context.Achievements.Where(a => a.GameId == 4).ToListAsync();
            foreach (var achievement in achievements)
            {
                if (!user.CompletedChallangesId.Contains(achievement.Id) && achievement.Requirement <= CompletedWords)
                {
                    user.CompletedChallangesId.Add(achievement.Id);
                    user.Coins = user.Coins + achievement.PrizeCoin;
                }
            }
            return Result<bool>.Ok(true);
        }
    }
}
