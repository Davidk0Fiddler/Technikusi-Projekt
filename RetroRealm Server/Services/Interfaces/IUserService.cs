using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using System.Runtime.InteropServices.Marshalling;

namespace RetroRealm_Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> CreateNewUserAsync(RegisterDTO usermodel);
        Task<Result<bool>> AddStatusesToUserAsync(string username);
        Task<Result<ReadUserDTO>> GetUserData(string username);
    }
}
