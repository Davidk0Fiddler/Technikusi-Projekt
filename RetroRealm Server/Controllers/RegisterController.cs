using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.Register;
using RetroRealm_Server.Services.Login_Service;
using RetroRealm_Server.Services.Register_Service;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _registerService.RegisterAsync(model);

            if (result.Success) return Created();

            if (result.Error == "User already exists") return BadRequest("User already exists!");

            return BadRequest();
        }
    }
}
