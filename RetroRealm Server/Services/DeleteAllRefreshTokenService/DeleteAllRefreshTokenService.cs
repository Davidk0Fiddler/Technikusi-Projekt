using RetroRealm_Server.Models;
using RetroRealm_Server.Services.LogService;

namespace RetroRealm_Server.Services.DeleteAllRefreshTokenService
{
    public class DeleteAllRefreshTokenService : IDeleteAllRefreshTokenService
    {
        private readonly RetroRealmDatabaseContext _context;
        private readonly ILogService _logService;
        public DeleteAllRefreshTokenService(RetroRealmDatabaseContext context,ILogService logService)
        {
            _context = context;
            _logService = logService;
        }
        public async Task<Result<bool>> DeleteAllRefreshTokenns()
        {
            _context.RefreshTokens.RemoveRange(_context.RefreshTokens);
            await _context.SaveChangesAsync();

            await _logService.CreateLogAsync(LogType.Succes.ToString(), null, $"All refresh tokens have been deleted!", DateTime.Now, null);
            return Result<bool>.Ok(true);
        }
    }
}
