using RetroRealm_Server.DTOs;
using RetroRealm_Server.DTOs.Login;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.RefreshTokenService
{
    public interface IRefreshTokenService
    {
        Task<Result<OutputTokenDTO>> RefreshTokenAsync(RefreshTokenDto model);
    }
}
