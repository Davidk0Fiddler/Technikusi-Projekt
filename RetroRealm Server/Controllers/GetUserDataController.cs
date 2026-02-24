using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.GetUserDataDTOs;
using RetroRealm_Server.Services.UserService;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUserDataController : ControllerBase
    {
        private readonly IUserService _userService;
        public GetUserDataController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<ReadUserDTO>> GetUserData(GetUserDataDTO userData)
        {
            var result = await _userService.GetUserDataAsync(userData.userName);

            if (result.Error == "User not found") return NotFound();

            return Ok(result.Data);
        }
    }
}
