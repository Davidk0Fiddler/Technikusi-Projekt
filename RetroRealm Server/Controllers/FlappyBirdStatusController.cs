using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlappyBirdStatusController : ControllerBase
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly IFlappyBirdStatusService _flappyBirdStatusService;

        public FlappyBirdStatusController(RetroRealmDatabaseContext context, IFlappyBirdStatusService flappyBirdStatusService)
        {
            _context = context;
            _flappyBirdStatusService = flappyBirdStatusService;
        }

        // GET: api/FlappyBirdStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ReadFlappyBirdStatusDTO>>> GetFlappyBirdStatus()
        {
            var result = await _flappyBirdStatusService.GetFlappyBirdStatusAsync(User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);
            if (result.Error == "Status not found!") return NotFound();

            //if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

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

            //if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return BadRequest();
            
        }
      
    }
}
