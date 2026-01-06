using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services;
using RetroRealm_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementsService _achievementsService;

        public AchievementsController(IAchievementsService achievementsService)
        {
            _achievementsService = achievementsService;
        }

        // GET: api/Achievements
        [HttpGet]
        public async Task<ActionResult<List<ReadAchievementDTO>>> GetAchievements()
        {
            var result = await _achievementsService.GetAllAchievementsAsync();

            if (!result.Success)
                return NotFound(new { error = result.Error });

            return Ok(result.Data);
        }

        // GET: api/Achievements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReadAchievementDTO>>> GetAchievement(int id)
        {
            var result = await _achievementsService.GetAchievementAsync(id);

            if (!result.Success) return NotFound();

            return Ok(result.Data);
        }

        // PUT: api/Achievements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAchievement(int id, UpdateAchievementDTO updatedAchievement)
        {
            if (id != updatedAchievement.Id)
            {
                return BadRequest();
            }

            var result = await _achievementsService.UpdateAchievementAsync(id, updatedAchievement);

            if (result.Success) return NoContent();

            if (result.Error == "Achievement not found!") return NotFound();

            return BadRequest();
        }

        // POST: api/Achievements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result<Achievement>>> PostAchievement(CreateAchievementDTO achievement)
        {
            var result = await _achievementsService.CreateAchievementAsync(achievement);

            if (!result.Success) return BadRequest();

            return CreatedAtAction("GetAchievement", new { id = achievement }, achievement);
        }

        // DELETE: api/Achievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(int id)
        {
            var result = await _achievementsService.DeleteAchievementByIdAsync(id);

            if (result.Success) return NoContent();

            if (result.Error == "Achievement not found!") return NotFound();

            return BadRequest();
        }
    }
}
