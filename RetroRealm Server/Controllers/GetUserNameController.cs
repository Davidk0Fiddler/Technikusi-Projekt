using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUserNameController : ControllerBase
    {
        public GetUserNameController()
        {
            
        }

        [HttpGet]
        [Authorize]
        public ActionResult<string> GetUserName()
        {
            var userName = User.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return NotFound("User name not found.");
            }
            return Ok(userName);
        }
    }
}
