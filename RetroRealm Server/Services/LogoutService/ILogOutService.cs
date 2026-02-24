using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.LogoutService
{
    public interface ILogOutService
    {
        Task<Result<bool>> LogoutAsync(RefreshTokenDto model);
    }
}
