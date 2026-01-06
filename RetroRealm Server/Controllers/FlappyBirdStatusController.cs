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
using System.Threading.Tasks;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Result<ReadFlappyBirdStatusDTO>>> GetFlappyBirdStatus(RefreshTokenDto model)
        {
            var result = await _flappyBirdStatusService.GetFlappyBirdStatusAsync(model);
            if (result.Error == "Status not found!") return NotFound();

            if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return Ok(result.Data);
        }

        // PUT: api/FlappyBirdStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutFlappyBirdStatus(UpdateFlappyBirdStatusDTO updatedStatus, RefreshTokenDto model)
        {
            var result = await _flappyBirdStatusService.UpdateFlappyBirdStatusAsync(updatedStatus, model);
            if (result.Success) return NoContent();

            if (result.Error == "Status not found") return NotFound();

            if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return BadRequest();
            
        }
      
    }
}
