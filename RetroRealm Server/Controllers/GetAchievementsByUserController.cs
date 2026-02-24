using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.GetAchievementsByUser;
using RetroRealm_Server.Services.GetAchievementsByUserService;
using System.Threading.Tasks;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAchievementsByUserController : ControllerBase
    {
        private readonly IGetAchievementsByUserService _getAchievementsByUserService;
        public GetAchievementsByUserController(IGetAchievementsByUserService getAchievementsByUserService)
        {
            _getAchievementsByUserService = getAchievementsByUserService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetAchievementsByUserDTO>>> GetAchievementsByUser(GetAchievementsByUserInputDTO input)
        {
            var result = await _getAchievementsByUserService.GetAchievementsByUser(input.UserName);

            if (!result.Success)
            {
                return NotFound();
            }

            return Ok(result.Data);
        }
    }
}
