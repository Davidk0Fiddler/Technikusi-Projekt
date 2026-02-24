using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs.Login;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Jwt_Service;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.Login_Service
{
    public class LoginService : ILoginService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly IJwtService _jwtService;

        public LoginService(RetroRealmDatabaseContext context, ILogService logService, IJwtService jwtService)
        {
            _context = context;
            _logService = logService;
            _jwtService = jwtService;
        }
        public async Task<Result<OutputTokenDTO>> LoginAsync(LoginDTO model)
        {
            // Getting the user from the database
            var user = await _context.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Username == model.Username);

            // When there is no user or the hashed password is wrong, login failed
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Invalid credentials", DateTime.Now, null);
                return Result<OutputTokenDTO>.Fail("Invalid credentials");
            }

            // Generating the tokens
            var token = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken(user.Id);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            // Succesfull login
            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Logged in! User:{model.Username}", DateTime.Now, null);
            return Result<OutputTokenDTO>.Ok(new OutputTokenDTO { Token = token, RefreshToken = refreshToken });
        }
    }
}
