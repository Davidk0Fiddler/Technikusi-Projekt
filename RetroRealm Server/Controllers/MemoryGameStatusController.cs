using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Interfaces;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<ReadMemoryGameStatusDTO>> GetMemoryGameStatus(RefreshTokenDto model)
        {
            var result = await _memoryGameStatusService.GetMemoryGameStatusAsync(model);

            if (result.Error == "Status not found") return NotFound();

            if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return Ok(result);
        }

        // PUT: api/MemoryGameStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutMemoryGameStatus(UpdateMemoryGameStatusDTO updatedStatus, RefreshTokenDto model)
        {
            var result = await _memoryGameStatusService.UpdateMemoryGameStatusAsync(updatedStatus, model);

            if (result.Success) return NoContent();

            if (result.Error == "Status not found") return NotFound();

            if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return BadRequest();
        }
    }
}
