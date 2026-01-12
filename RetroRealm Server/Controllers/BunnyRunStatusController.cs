using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services;
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


    public class BunnyRunStatusController : ControllerBase
    {


        private readonly RetroRealmDatabaseContext _context;
        private readonly IBunnyRunStatusService _bunnyRunStatusService;

        public BunnyRunStatusController(RetroRealmDatabaseContext context, IBunnyRunStatusService bunnyRunStatusService)
        {
            _context = context;
            _bunnyRunStatusService = bunnyRunStatusService;
        }


        // GET: api/BunnyRunStatus/5
        [HttpGet]
        public async Task<ActionResult<ReadBunnyRunStatusDTO>> GetBunnyRunStatus()
        {
            var result = await _bunnyRunStatusService.GetBunnyRunStatusAsync(User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);

            if (result.Error == "Status not found!") return NotFound();
             
            return Ok(result.Data);
        }

        // PUT: api/BunnyRunStatus/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutBunnyRunStatus(UpdateBunnyRunStatusDTO updatedStatus)
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            var result = await _bunnyRunStatusService.UpdateBunnyRunStatusAsync(updatedStatus, User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value);

            if (result.Success) return NoContent();

            //if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            if (result.Error == "Status not found") return NotFound();

            return BadRequest();
        }
    }
}
