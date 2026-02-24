using RetroRealm_Server.DTOs.GetUserDataDTOs;
using RetroRealm_Server.DTOs.Register;
using RetroRealm_Server.Models;

namespace RetroRealm_Server.Services.UserService
{
    public interface IUserService
    {
        Task<Result<User>> CreateNewUserAsync(RegisterDTO usermodel);
        Task<Result<bool>> AddStatusesToUserAsync(string username);
        Task<Result<ReadUserDTO>> GetUserDataAsync(string userName);
    }
}
