using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Services.RefreshTokenService;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshtokenController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;
        public RefreshtokenController(IRefreshTokenService refreshTokenService) 
        { 
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            var result = await _refreshTokenService.RefreshTokenAsync(model);

            if (result.Success) return Ok(result.Data);

            if (result.Error == "Invalid refresh token") return Unauthorized("Invalid refresh token");

            return Unauthorized("Expired refresh token");

        }
    }
}
