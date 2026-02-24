using RetroRealm_Server.DTOs.LeaderBoardDTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.LeaderboardService
{
    public interface ILeaderboardService
    {
        Task<Result<List<BunnyRunLeaderboardElementDTO>>> GetBunnyRunLeaderboardAsync();
        Task<Result<List<FlappyBirdLeaderboardElementDTO>>> GetFlappyBirdLeaderboardAsync();
        Task<Result<List<MemoryGameLeaderboardElementDTO>>> GetMemoryGameLeaderboardByFlipsAsync();
        Task<Result<List<MemoryGameLeaderboardElementDTO>>> GetMemoryGameLeaderboardByTimeAsync();
        Task<Result<List<WordleLeaderboardElementDTO>>> GetWordleLeaderboardAsync();    
        Task<Result<List<AchievementsLeaderboardElementDTO>>> GetAchievementsLeaderboardAsync();    
    }
}
