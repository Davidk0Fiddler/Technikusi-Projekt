using RetroRealm_Server.DTOs.GetAvatarsForUserDTO;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.GetAvatarsForUserService
{
    public interface IGetAvatarsForUserService
    {
        Task<Result<List<GetAvatarForUserDTO>>> GetAvatarsForUserAsync(string userName);
    }
}
