using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs.GetAchievementsByUser;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.GetAchievementsByUserService
{
    public class GetAchievementsByUserService : IGetAchievementsByUserService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;

        public GetAchievementsByUserService(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<Result<List<GetAchievementsByUserDTO>>> GetAchievementsByUser(string userName) {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName);
            if (user == null)
            {
                return Result<List<GetAchievementsByUserDTO>>.Fail("User not found!");
            }

            var achievements = await _context.Achievements.ToListAsync();

            var mappedAchievements = await AchievementDTOMapper(achievements, user.CompletedChallangesId);

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, $"Achievements of user-{user.Id} have been requested!",DateTime.Now, null);
            return Result<List<GetAchievementsByUserDTO>>.Ok(mappedAchievements);
        }

        public async Task<List<GetAchievementsByUserDTO>> AchievementDTOMapper(List<Achievement> achievements, List<int> completedAchievementsId) {
            var mappedList = new List<GetAchievementsByUserDTO>();

            foreach (var achievement in achievements)
            {
                var mappedAchievement = new GetAchievementsByUserDTO
                {
                    NameEng = achievement.NameEng,
                    NameEsp = achievement.NameEsp,
                    NameHun = achievement.NameHun,
                    DescriptionEng = achievement.DescriptionEng,
                    DescriptionEsp = achievement.DescriptionEsp,
                    DescriptionHun = achievement.DescriptionHun,
                    GameId = achievement.GameId,
                    PrizeCoin = achievement.PrizeCoin,
                    Requirement = achievement.Requirement,
                    IsAchieved = completedAchievementsId.Contains(achievement.Id) ? true : false,
                };

                mappedList.Add(mappedAchievement);
            }

            return mappedList; 
        }
    }
}
