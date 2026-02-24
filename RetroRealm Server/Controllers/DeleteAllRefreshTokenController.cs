using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.Services.DeleteAllRefreshTokenService;

namespace RetroRealm_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteAllRefreshTokenController : ControllerBase
    {
        private readonly IDeleteAllRefreshTokenService _deleteAllRefreshTokenService;

        public DeleteAllRefreshTokenController(IDeleteAllRefreshTokenService deleteAllRefreshTokenService)
        {
            _deleteAllRefreshTokenService = deleteAllRefreshTokenService;
        }

        [HttpDelete()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAllRefreshTokens()
        {
            await _deleteAllRefreshTokenService.DeleteAllRefreshTokenns();
            return NoContent();
        }
    }
}
