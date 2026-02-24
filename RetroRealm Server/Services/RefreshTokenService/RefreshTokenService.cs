using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.DTOs.Login;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.Jwt_Service;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.RefreshTokenService
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        private readonly IJwtService _jwtService;

        public RefreshTokenService(RetroRealmDatabaseContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<Result<OutputTokenDTO>> RefreshTokenAsync(RefreshTokenDto model)
        {
            // Getting the refresh token
            var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == model.Token);
            
            // Returning error when there is no refresh token
            if (refreshToken == null)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Invalid refresh token", DateTime.Now, null);
                return Result<OutputTokenDTO>.Fail("Invalid refresh token");
            }

            // Returning error when the token has expired
            if (refreshToken.Expires < DateTime.UtcNow)
            {
                await _logService.CreateLogAsync(LogType.Error.ToString(), null, "Expired refresh token", DateTime.Now, null);
                return Result<OutputTokenDTO>.Fail("Expired refresh token");
            }

            // Generating new tokens
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == refreshToken.UserId);

            var newToken = _jwtService.GenerateJwtToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken(refreshToken.User.Id);

            // Deleting the old and adding the new refresh token
            _context.RefreshTokens.Remove(refreshToken);
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            // Returing the new refresh token
            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"Token refreshed! UserId - {newRefreshToken.UserId}", DateTime.Now, newRefreshToken.UserId);
            return Result<OutputTokenDTO>.Ok(new OutputTokenDTO { Token = newToken, RefreshToken = newRefreshToken });
        }
    }
}
