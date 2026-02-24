using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.PurchaseAvatarDTO;
using RetroRealm_Server.Services.AvatarService;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseAvatarController : ControllerBase
    {
        private readonly IAvatarService _avatarService;
        public PurchaseAvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PurchaseAvatar(PurchaseAvatarDTO avatar)
        {
            var result = await _avatarService.PurchaseAvatarAsync(User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value, avatar.AvatarName);
            if (result.Success) return Ok(result.Data);
            if (result.Error == "Avatar not found!") return NotFound();
            if (result.Error == "User not found!") return NotFound();
            if (result.Error == "Not enough coins!") return BadRequest("Not enough coins!");
            return BadRequest();
        }
    }
}
