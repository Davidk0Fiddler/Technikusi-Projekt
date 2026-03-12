using RetroRealm_Server.DTOs;
using RetroRealm_Server.DTOs.Login;
using RetroRealm_Server.DTOs.Register;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services._NotUserServices.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> RegisterAsync(RegisterDTO model);
        Task<Result<OutputTokenDTO>> LoginAsync(LoginDTO model);
        Task<Result<OutputTokenDTO>> RefreshTokenAsync(RefreshTokenDto model);
        Task<Result<bool>> LogoutAsync(RefreshTokenDto model);
        Task<Result<bool>> DeleteAllRefreshTokenns();
    }
}
