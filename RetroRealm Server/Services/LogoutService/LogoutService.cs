using Microsoft.EntityFrameworkCore;
using RetroRealm_Server.DTOs;
using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.LogoutService
{
    public class LogoutService : ILogOutService
    {
        private readonly ILogService _logService;
        private readonly RetroRealmDatabaseContext _context;

        public LogoutService(RetroRealmDatabaseContext context, ILogService logService)
        { 
            _context = context;
            _logService = logService;
        }
        public async Task<Result<bool>> LogoutAsync(RefreshTokenDto model)
        {
            //Getting the refreshToken
            var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == model.Token);


            // Deleting the refreshToken if exists
            if (refreshToken != null)
            {
                await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"User {refreshToken.UserId} has logged out!", DateTime.Now, refreshToken.UserId);
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }

            return Result<bool>.Ok(true);
        }
    }
}
