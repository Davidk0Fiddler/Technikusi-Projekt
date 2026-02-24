using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.WorldeStatusDTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.WorldeStatusService;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WordleStatusController : ControllerBase
    {
        private readonly IWordleStatusService _wordleStatusService;

        public WordleStatusController(IWordleStatusService wordleStatusService)
        {
            _wordleStatusService = wordleStatusService;
        }

        // GET: api/WordleStatus/5
        [HttpGet]
        public async Task<ActionResult<ReadWordleStatusDTO>> GetWordleStatus()
        {
            var result = await _wordleStatusService.GetWordleStatusAsync(User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);

            if (result.Error == "Status not found!") return NotFound();

            return Ok(result.Data);
        }

        // PUT: api/WordleStatus/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutWordleStatus(UpdateWordleStatusDTO updatedStatus)
        {
            var result = await _wordleStatusService.UpdateWordleStatusAsync(updatedStatus, User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);

            if (result.Success) return NoContent();

            if (result.Error == "Status not found") return NotFound();

            return BadRequest();
        }

    }
}
