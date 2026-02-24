using Microsoft.AspNetCore.Mvc;
using RetroRealm_Server.DTOs._NotUserDTOS;
using RetroRealm_Server.Services.AvatarService;

namespace RetroRealm_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarsController : ControllerBase
    {
        private readonly IAvatarService _avatarService;

        public AvatarsController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        // GET: api/Avatars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<ReadAvatarDTO>>>> GetAvatars()
        {
            var result = await _avatarService.GetAllAvatarsAsync();

            if (result.Success) return Ok(result.Data);

            return BadRequest();
        }

        //        // GET: api/Avatars/5
        //        [HttpGet("{id}")]
        //        public async Task<ActionResult<IEnumerable<ReadAvatarDTO>>> GetAvatar(int id)
        //        {
        //            var result = await _avatarService.GetAvatarAsync(id);

        //            if (result.Success) return Ok(result.Data);

        //            return NotFound();
        //        }

        //        // PUT: api/Avatars/5
        //        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //        [HttpPut("{id}")]
        //        public async Task<IActionResult> PutAvatar(int id, UpdateAvatarDTO avatar)
        //        {
        //            if (id != avatar.Id)
        //            {
        //                return BadRequest();
        //            }

        //            var result = await _avatarService.UpdateAvatarAsync(id, avatar);

        //            if (result.Success) return NoContent();

        //            if (result.Error == "Avatar not found!") return NotFound();

        //            return BadRequest();
        //        }

        //        //POST: api/Avatars
        //        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //        [HttpPost]
        //        public async Task<ActionResult<Avatar>> PostAvatar(CreateAvatarDTO avatar)
        //        {
        //            var result = await _avatarService.CreateAvatarAsync(avatar);

        //            if (result.Success) return CreatedAtAction("GetAvatar", new { id = avatar }, avatar);

        //            return BadRequest();
        //        }

        //        //DELETE: api/Avatars/5
        //        [HttpDelete("{id}")]
        //        public async Task<IActionResult> DeleteAvatar(int id)
        //        {
        //            var result = await _avatarService.DeleteAvatarByIdAsync(id);

        //            if(result.Success) return NoContent();

        //            if (result.Error == "Avatar not found!") return NotFound();

        //            return BadRequest();
        //        }
        //    }
    }
}
