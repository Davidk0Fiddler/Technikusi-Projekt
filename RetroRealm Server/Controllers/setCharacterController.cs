using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.SetCharacterDTO;
using RetroRealm_Server.Services.SetCharacterService;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class setCharacterController : ControllerBase
    {
        private readonly ISetCharacterService _setCharacterService;
        public setCharacterController(ISetCharacterService setCharacterService)
        {
            _setCharacterService = setCharacterService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SetCharacter(SetCharacterDTO setCharacterDTO)
        {
            var userName = User.Claims.FirstOrDefault(c => c.Type == "unique_name" || c.Type.EndsWith("/name"))?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return NotFound("User name not found.");
            }

            var result = await _setCharacterService.SetCharacter(setCharacterDTO.characterName, userName);

            if (result.Success) return Ok();
            
            return BadRequest(result.Error);
        }
    }
}
