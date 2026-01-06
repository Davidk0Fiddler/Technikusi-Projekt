using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemoryGameStatusController : ControllerBase
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly IMemoryGameStatusService _memoryGameStatusService;

        public MemoryGameStatusController(RetroRealmDatabaseContext context, IMemoryGameStatusService memoryGameStatusService)
        {
            _context = context;
            _memoryGameStatusService = memoryGameStatusService;
        }

        // GET: api/MemoryGameStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMemoryGameStatusDTO>> GetMemoryGameStatus()
        {
            var result = await _memoryGameStatusService.GetMemoryGameStatusAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            if (result.Error == "Status not found") return NotFound();

            //if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return Ok(result);
        }

        // PUT: api/MemoryGameStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutMemoryGameStatus(UpdateMemoryGameStatusDTO updatedStatus)
        {
            var result = await _memoryGameStatusService.UpdateMemoryGameStatusAsync(updatedStatus, User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            if (result.Success) return NoContent();

            if (result.Error == "Status not found") return NotFound();

            //if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return BadRequest();
        }
    }
}
