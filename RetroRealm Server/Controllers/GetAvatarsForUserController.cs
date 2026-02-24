using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.GetAchievementsByUser;
using RetroRealm_Server.Services.GetAchievementsByUserService;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAvatarsForUserController : ControllerBase
    {
        private readonly IGetAchievementsByUserService _getAchievementsByUserService;
        public GetAvatarsForUserController(IGetAchievementsByUserService getAchievementsByUserService)
        {
            _getAchievementsByUserService = getAchievementsByUserService;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetAchievementsByUserDTO>>> GetAvatarsForUser()
        {
            var userName = User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            var result = await _getAchievementsByUserService.GetAchievementsByUser(userName);
            
            if (!result.Success)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }
    } 
}
