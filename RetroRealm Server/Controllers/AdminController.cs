using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.AdminPanelDTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.AdminService;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("/getName")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> GetUserName()
        {
            var userName = User.Claims.FirstOrDefault(c => c.Type == "unique_name" || c.Type.EndsWith("/name"))?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return NotFound("User name not found.");
            }
            return Ok(userName);
        }

        [HttpGet("/users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ReadUserForAdminDTO>>> GetUsersForAdminPanel() { 
            var result = await _adminService.GetAllUsersForAdminPanel();

            return Ok(result.Data);
        }

        [HttpGet("/logs")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ReadLogDTO>>> GetLogsForAdminPanel()
        {
            var result = await _adminService.GetAllLogsForAdminPanel();

            return Ok(result.Data);
        }
    }
}
