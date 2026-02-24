using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs.LeaderBoardDTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LeaderboardService;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.LeaderBoardService
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        public LeaderboardService(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<Result<List<BunnyRunLeaderboardElementDTO>>> GetBunnyRunLeaderboardAsync()
        {

            var statuses = await _context.Bunny_Run_Status.Include(s => s.User).ToListAsync();

            var exitLeaderBoard = await BunnyRunLeaderboardMapper(statuses);

            exitLeaderBoard = exitLeaderBoard.OrderByDescending(s => s.MaxDistance).ToList();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "BunnyRun leaderboard requested!", DateTime.Now, null);
            return Result<List<BunnyRunLeaderboardElementDTO>>.Ok(exitLeaderBoard);
        }

        public async Task<Result<List<FlappyBirdLeaderboardElementDTO>>> GetFlappyBirdLeaderboardAsync()
        {

            var statuses = await _context.Flappy_Bird_Status.Include(s => s.User).ToListAsync();

            var exitLeaderBoard = await FlappyBirdLeaderboardMapper(statuses);

            exitLeaderBoard = exitLeaderBoard.OrderByDescending(s => s.MaxPassedPipes).ToList();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "FlappyBird leaderboard requested!", DateTime.Now, null);
            return Result<List<FlappyBirdLeaderboardElementDTO>>.Ok(exitLeaderBoard);
        }

        public async Task<Result<List<MemoryGameLeaderboardElementDTO>>> GetMemoryGameLeaderboardByTimeAsync()
        {
            var statuses = await _context.Memory_Game_Status.Include(s => s.User).ToListAsync();
            
            var exitLeaderboard = await MemoryGameLeaderboardMapper(statuses);

            exitLeaderboard = exitLeaderboard.OrderBy(s => new TimeSpan(s.MinTime[0],s.MinTime[1],s.MinTime[2])).ToList();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "MemoryGame leaderboard (time) requested!", DateTime.Now, null);
            return Result<List<MemoryGameLeaderboardElementDTO>>.Ok(exitLeaderboard);
        }

        public async Task<Result<List<MemoryGameLeaderboardElementDTO>>> GetMemoryGameLeaderboardByFlipsAsync()
        {
            var statuses = await _context.Memory_Game_Status.Include(s => s.User).ToListAsync();

            var exitLeaderboard = await MemoryGameLeaderboardMapper(statuses);

            exitLeaderboard = exitLeaderboard.OrderBy(s => s.MinFlipping).ToList();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "MemoryGame leaderboard (flips) requested!", DateTime.Now, null);
            return Result<List<MemoryGameLeaderboardElementDTO>>.Ok(exitLeaderboard);
        }

        public async Task<Result<List<WordleLeaderboardElementDTO>>> GetWordleLeaderboardAsync() {
            var statuses = await _context.Wordle_Status.Include(s => s.User).ToListAsync();

            var exitLeaderboard = await WordleLeaderboardMapper(statuses);

            exitLeaderboard = exitLeaderboard.OrderByDescending(s => s.CompletedWords).ToList();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "Wordle leaderboard requested!", DateTime.Now, null);
            return Result<List<WordleLeaderboardElementDTO>>.Ok(exitLeaderboard);
        }

        public async Task<Result<List<AchievementsLeaderboardElementDTO>>> GetAchievementsLeaderboardAsync() {
            var users = await _context.Users.ToListAsync();

            var existingList = await AchievementLeaderboardMapper(users);

            existingList = existingList.OrderByDescending(e => e.CompletedAchievements).ToList();

            await _logService.CreateLogAsync(LogType.Get.ToString(), null, "Achievement leaderboard requested!", DateTime.Now, null);
            return Result<List<AchievementsLeaderboardElementDTO>>.Ok(existingList);
        }



        private async Task<List<BunnyRunLeaderboardElementDTO>> BunnyRunLeaderboardMapper(List<BunnyRunStatus> statuses)
        {
            var mappedList = new List<BunnyRunLeaderboardElementDTO>();

            foreach (var status in statuses)
            {
                var mappedStatus = new BunnyRunLeaderboardElementDTO
                {
                    UserName = status.User.Username,
                    MaxDistance = status.MaxDistance,
                };

                mappedList.Add(mappedStatus);
            }

            return mappedList;
        }

        private async Task<List<FlappyBirdLeaderboardElementDTO>> FlappyBirdLeaderboardMapper(List<FlappyBirdStatus> statuses)
        {
            var mappedList = new List<FlappyBirdLeaderboardElementDTO>();
            foreach (var status in statuses)
            {
                var mappedStatus = new FlappyBirdLeaderboardElementDTO
                {
                    UserName = status.User.Username,
                    MaxPassedPipes = status.MaxPassedPipes,
                };
                mappedList.Add(mappedStatus);
            }
            return mappedList;
        }

        private async Task<List<MemoryGameLeaderboardElementDTO>> MemoryGameLeaderboardMapper(List<MemoryGameStatus> statuses)
        {
            var mappedList = new List<MemoryGameLeaderboardElementDTO>();
            foreach (var status in statuses)
            {
                var mappedStatus = new MemoryGameLeaderboardElementDTO
                {
                    UserName = status.User.Username,
                    MinTime = status.MinTime,
                    MinFlipping = status.MinFlipping,
                };
                mappedList.Add(mappedStatus);
            }
            return mappedList;
        }

        private async Task<List<WordleLeaderboardElementDTO>> WordleLeaderboardMapper(List<WordleStatus> statuses) {
            var mappedList = new List<WordleLeaderboardElementDTO>();

            foreach (var status in statuses)
            {
                var mappedStatus = new WordleLeaderboardElementDTO
                {
                    UserName = status.User.Username,
                    CompletedWords = status.CompletedWords,
                };
                mappedList.Add(mappedStatus);
            }
            return mappedList;
        }

        private async Task<List<AchievementsLeaderboardElementDTO>> AchievementLeaderboardMapper(List<User> users) {
            var mappedList = new List<AchievementsLeaderboardElementDTO>();

            foreach (var user in users)
            {
                var mappedItem = new AchievementsLeaderboardElementDTO
                {
                    UserName = user.Username,
                    CompletedAchievements = user.CompletedChallangesId.Count,
                };

                mappedList.Add(mappedItem);
            }

            return mappedList;
        }
    }
}