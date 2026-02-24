using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.FlappyBirdDTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.FlappyBirdService;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlappyBirdStatusController : ControllerBase
    {
        private readonly IFlappyBirdStatusService _flappyBirdStatusService;

        public FlappyBirdStatusController(IFlappyBirdStatusService flappyBirdStatusService)
        {
            _flappyBirdStatusService = flappyBirdStatusService;
        }

        // GET: api/FlappyBirdStatus
        [HttpGet]
        public async Task<ActionResult<Result<ReadFlappyBirdStatusDTO>>> GetFlappyBirdStatus()
        {
            var result = await _flappyBirdStatusService.GetFlappyBirdStatusAsync(User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);
            if (result.Error == "Status not found!") return NotFound();

            return Ok(result.Data);
        }

        // PUT: api/FlappyBirdStatus/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutFlappyBirdStatus(UpdateFlappyBirdStatusDTO updatedStatus)
        {
            var result = await _flappyBirdStatusService.UpdateFlappyBirdStatusAsync(updatedStatus, User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);
            if (result.Success) return NoContent();

            if (result.Error == "Status not found") return NotFound();

            return BadRequest();
            
        }
      
    }
}
