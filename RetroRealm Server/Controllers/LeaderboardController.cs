using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.LeaderBoardDTOs;
using RetroRealm_Server.Services.LeaderboardService;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;
        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [HttpGet("/bunnyrun")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<List<BunnyRunLeaderboardElementDTO>>>> GetBunnyRunLeaderboardAsync()
        {
            var result = await _leaderboardService.GetBunnyRunLeaderboardAsync();
            return Ok(result.Data);
        }

        [HttpGet("/flappybird")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<List<FlappyBirdLeaderboardElementDTO>>>> GetFlappyBirdLeaderboardAsync() { 
            var result = await _leaderboardService.GetFlappyBirdLeaderboardAsync();
            return Ok(result.Data);
        }

        [HttpGet("/memorygame/flips")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<List<MemoryGameLeaderboardElementDTO>>>> GetMemoryGameFlipsLeaderboardAsync() {
            var result = await _leaderboardService.GetMemoryGameLeaderboardByFlipsAsync();
            return Ok(result.Data);
        }

        [HttpGet("/memorygame/time")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<List<MemoryGameLeaderboardElementDTO>>>> GetMemoryGameTimeLeaderboardAsync() {
            var result = await _leaderboardService.GetMemoryGameLeaderboardByTimeAsync();
            return Ok(result.Data);
        }

        [HttpGet("/wordle")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<List<WordleLeaderboardElementDTO>>>> GetWordleLeaderboardAsync() { 
            var result = await _leaderboardService.GetWordleLeaderboardAsync();
            return Ok(result.Data);
        }

        [HttpGet("/achievement")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<List<AchievementsLeaderboardElementDTO>>>> GetAchievementLeaderboardAsync()
        {
            var result = await _leaderboardService.GetAchievementsLeaderboardAsync();
            return Ok(result.Data);
        }
    }
}
