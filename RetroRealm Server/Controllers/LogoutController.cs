using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Services.LogoutService;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly ILogOutService _logOutService;
        public LogoutController(ILogOutService logOutService) 
        { 
            _logOutService = logOutService;
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto model)
        {
            var result = _logOutService.LogoutAsync(model);
            return Ok(new { message = "Logged out successfully." });
        }
    }
}
