using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.GetAchievementsDTO;
using RetroRealm_Server.Services._NotUserServices.Interfaces;
using RetroRealm_Server.Services.GetAchievementsService.cs;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAchievementsController : ControllerBase
    {
        private readonly IGetAchievementsService _achievementsService;
        public GetAchievementsController(IGetAchievementsService achievementsService)
        {
            _achievementsService = achievementsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAchievementDTO>>> GetAchievements()
        {
            var result = await _achievementsService.GetAchievementsAsync();
            if (!result.Success)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Data);
        }
    }
}
