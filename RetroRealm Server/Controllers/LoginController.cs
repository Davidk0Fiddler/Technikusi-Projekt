using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs.Login;
using RetroRealm_Server.Services.Login_Service;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        // Connecting the dependencies
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost()]
        [AllowAnonymous] // Allow anonymus users to login
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            // Passing the business logic to the Login Service.
            var result = await _loginService.LoginAsync(model);

            // Result if the login was succesfull.
            if (result.Success) return Ok(result.Data);

            // Result if the login was unsuccesfull.
            return Unauthorized("Invalid credentials.");

        }
    }
}
