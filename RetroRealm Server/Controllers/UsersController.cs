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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/FlappyBirdStatus
        [HttpPost("getuserdata")]
        public async Task<ActionResult<Result<ReadUserDTO>>> GetUserInfo(RefreshTokenDto model)
        {
            var result = await _userService.GetUserData(model);
            if (result.Error == "Status not found!") return NotFound();

            //if (result.Error == "RefreshToken expired or does not exists!") return Unauthorized();

            return Ok(result.Data);
        }
    }
}
