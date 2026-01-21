using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> RegisterAsync(RegisterDTO model);
        Task<Result<ReadTokenDTO>> LoginAsync(LoginDTO model);
        Task<Result<ReadTokenDTO>> RefreshTokenAsync(RefreshTokenDto model);
        Task<Result<bool>> LogoutAsync(RefreshTokenDto model);
        Task<Result<bool>> DeleteAllRefreshTokenns();
    }
}
