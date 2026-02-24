using RetroRealm_Server.DTOs.Register;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.Register_Service
{
    public interface IRegisterService
    {
        Task<Result<string>> RegisterAsync(RegisterDTO model);
    }
}
