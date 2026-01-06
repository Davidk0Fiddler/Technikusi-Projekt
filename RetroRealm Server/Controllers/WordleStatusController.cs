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
    public class WordleStatusController : ControllerBase
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly IWordleStatusService _wordleStatusService;

        public WordleStatusController(RetroRealmDatabaseContext context, IWordleStatusService wordleStatusService)
        {
            _context = context;
            _wordleStatusService = wordleStatusService;
        }

        // GET: api/WordleStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadWordleStatusDTO>> GetWordleStatus(RefreshTokenDto model)
        {
            var result = await _wordleStatusService.GetWordleStatusAsync(model);

            if (result.Error == "Status not found!") return NotFound();

            if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return Ok(result.Data);
        }

        // PUT: api/WordleStatus/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutWordleStatus(UpdateWordleStatusDTO updatedStatus, RefreshTokenDto model)
        {
            var result = await _wordleStatusService.UpdateWordleStatusAsync(updatedStatus, model);

            if (result.Success) return NoContent();

            if (result.Error == "Status not found") return NotFound();

            if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return BadRequest();
        }

    }
}
