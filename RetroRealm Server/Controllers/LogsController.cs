using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class LogsController : ControllerBase
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;

        public LogsController(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        // GET: api/Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result<List<Log>>>>> GetLogs()
        {
            var result = await _logService.GetAllLogsAsync();


            return Ok(result.Data);
        }

        // GET: api/Logs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<Log>>> GetLog(int id)
        {
            var result = await _logService.GetLogAsync(id);

            if (!result.Success) return NotFound();

            return Ok(result.Data);
        }
    }
}
