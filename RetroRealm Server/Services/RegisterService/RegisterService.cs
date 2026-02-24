using RetroRealm_Server.DTOs.Register;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.UserService;

namespace RetroRealm_Server.Services.Register_Service
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserService _userService;

        public RegisterService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<string>> RegisterAsync(RegisterDTO model)
        {
            var result = await _userService.CreateNewUserAsync(model);

            if (result.Error == "User already exists") return Result<string>.Fail("User already exists");

            if (result.Error == "Database error") return Result<string>.Fail("Database error");

            if (result.Error == "Error") return Result<string>.Fail("Error");

            var addingresult = await _userService.AddStatusesToUserAsync(result.Data.Username);

            if (addingresult.Error == "Error during creating statuses!") return Result<string>.Fail("Error during creating statuses!");

            if (addingresult.Error == "Database error") return Result<string>.Fail("Database error");

            if (addingresult.Error == "Error") return Result<string>.Fail("Error");

            return Result<string>.Ok("User registered successfully");
        }
    }
}
