using RetroRealm_Server.DTOs.Login;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Login_Service
{
    public interface ILoginService
    {
        Task<Result<OutputTokenDTO>> LoginAsync(LoginDTO model);
    }
}
